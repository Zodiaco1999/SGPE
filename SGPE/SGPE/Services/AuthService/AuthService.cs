using AutoMapper;
using SGPE.Comun.ContextAccesor;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Helpers;
using SGPE.Comun.JWT;
using SGPE.WebApi.Services.UsuarioService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SGPE.WebApi.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly SGPEContext _db;
    private readonly IConfiguration _configuration;
    private readonly IUsuarioService _usuarioService;
    private readonly IContextAccessor _contextAccessor;
    private readonly ICorreoService _correoService;
    private readonly IJWTFactory _jwtFactory;
    private readonly IMapper _mapper;

    public AuthService(
        SGPEContext db,
        IConfiguration configuration,
        IUsuarioService usuarioService,
        IContextAccessor contextAccessor,
        ICorreoService correoService,
        IJWTFactory jwtFactory,
        IMapper mapper)
    {
        _db = db;
        _configuration = configuration;
        _usuarioService = usuarioService;
        _contextAccessor = contextAccessor;
        _correoService = correoService;
        _jwtFactory = jwtFactory;
        _mapper = mapper;
    }

    public async Task<LoginResult> Login(UsuarioLoginDto dtoAuth)
    {
        var user = await _usuarioService.GetUsuarioByCedula(dtoAuth.CedulaUsuario);

        if (!HashHelper.CheckHash(dtoAuth.Contrasena, user.PasswordHash, user.PasswordSalt))
            throw new ValidationException("Usuario o Contraseña incorrectos");

        string sesionId = Guid.NewGuid().ToString();
        string refreshToken = Guid.NewGuid().ToString();

        return new LoginResult
        {
            Usuario = _mapper.Map<UsuarioDto>(user),
            Jwt = CreateJWT(user, sesionId, refreshToken),
            TokenSession = sesionId
        };
    }

    private string CreateJWT(Usuario user, string sesionId, string tokenRefresh)
    {
        var claimsIdentity = GetClaimsIdentity(user, sesionId);

        return _jwtFactory.GenerateEncodedToken(user.CedulaUsuario, claimsIdentity, tokenRefresh);
    }

    private ClaimsIdentity GetClaimsIdentity(Usuario usuario, string sesionId)
    {
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaims(new Claim[]
        {
            new(JwtRegisteredClaimNames.NameId, usuario.CedulaUsuario),
            new(JwtRegisteredClaimNames.UniqueName, usuario.IdUsuario.ToString()),
            new(JwtRegisteredClaimNames.Jti, sesionId),
            new(JwtRegisteredClaimNames.Actort, $"{usuario.Nombres} {usuario.Apellidos}"),
            new(JwtRegisteredClaimNames.Email, usuario.Correo ?? "")
        });

        return claimsIdentity;
    }

    public async Task<string> ChangePassword(ChangePasswordDto userPassword)
    {
        var user = await _usuarioService.GetUsuarioById(Guid.Parse(_contextAccessor.UserId));

        if (userPassword.ContrasenaActual == userPassword.ContrasenaNueva)
            throw new ValidationException("La contraseña actual no puede ser igual a la nueva");
        else if (!HashHelper.CheckHash(userPassword.ContrasenaActual, user.PasswordHash, user.PasswordSalt))
            throw new ValidationException("Contraseña Actual Invalida");
        else
        {
            var hash = HashHelper.Hash(userPassword.ContrasenaNueva);
            user.PasswordHash = hash.Password;
            user.PasswordSalt = hash.Salt;

            await _db.SaveChangesAsync();
        }

        return "Contraseña cambiada correctamente";
    }

    public async Task<string> SendEmailChangeEmail(string email)
    {
        var existeCorreo = await _usuarioService.EmailExists(email);
        if (existeCorreo)
            throw new ValidationException($"El correo: {email} ya esta registrado");

        var user = await _usuarioService.GetUsuarioById(Guid.Parse(_contextAccessor.UserId));
        user.Token = CreateRandomToken();

        string link = $"{_configuration.GetValue<string>("Client:Url")}/usuarios/restablecercorreo/{email}/{user.Token}";
        await _correoService.SendEmail(
            "Cambiar correo",
            $"SGPE<br><br>Click <a href=\"{link}\">aqui</a> para finalizar el proceso de reestablecer tu correo",
            new string[] { email },
            new string[] { });

        await _db.SaveChangesAsync();

        return "Revisa tu correo electrónico para finalizar el proceso";
    }

    public async Task<string> ChangeEmail(string email, string token)
    {
        var existeCorreo = await _usuarioService.EmailExists(email);
        if (existeCorreo)
            throw new ValidationException($"El correo: {email} ya esta registrado");

        var user = await _usuarioService.GetUsuarioById(Guid.Parse(_contextAccessor.UserId));

        if (user.Token != token)
            throw new ValidationException("No se pudo concluir el proceso de reestablecer correo");

        user.Correo = email;
        user.Token = null;
        await _db.SaveChangesAsync();

        return "Correo electrónico establecido correctamente";
    }

    public async Task<string> SendEmailResetPassword(string cedula)
    {
        var user = await _usuarioService.GetUsuarioByCedula(cedula);

        if (string.IsNullOrEmpty(user.Correo))
            throw new NotFoundException("Su usuario no tiene correo electrónico, por favor comuniquese con soporte");

        string token = CreateRandomToken();
        string link = $"{_configuration.GetSection("Client:Url").Value}/usuarios/restablecercontrasena/{user.Correo}/{token}";
        int timeExpiration = _configuration.GetValue<int>("TimeExpirationToken");

        await _correoService.SendEmail(
            "Restablecer contraseña",
            $"SGPE<br><br>Click <a href=\"{link}\">aqui</a> para finalizar el proceso de reestablecer tu contraseña" +
            $"<br><br>Este enlace estara activo durante {timeExpiration} minutos",
            new string[] { user.Correo },
            new string[] { });

        user.Token = token;
        user.FechaExpiracionToken = DateTime.Now.AddMinutes(timeExpiration);
        await _db.SaveChangesAsync();

        return $"Para continuar el proceso de restablecer contraseña, revisa tu correo electrónico: {user.Correo}";
    }

    public async Task<string> ResetPassword(ResetPasswordDto userPassword, string token)
    {
        var user = await VerifyTokenAndGetUser(userPassword.Correo, token);

        var hash = HashHelper.Hash(userPassword.ContrasenaNueva);
        user.PasswordHash = hash.Password;
        user.PasswordSalt = hash.Salt;
        user.Token = null;
        user.FechaExpiracionToken = null;

        await _db.SaveChangesAsync();

        return "¡Contraseña restablecida correctamente!";
    }

    public async Task<Usuario> VerifyTokenAndGetUser(string correo, string token)
    {
        var user = await _usuarioService.GetUserByEmail(correo);
        if (user.Token != token || user.FechaExpiracionToken < DateTime.Now)
            throw new ValidationException("El tiempo para restablecer la contraseña caducó, por favor vuelva a iniciar el proceso.");

        return user;
    }

    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}
