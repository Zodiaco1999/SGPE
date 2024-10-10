using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SGPE.Comun.ContextAccesor;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Extensions;
using SGPE.Comun.Helpers;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.UsuarioService.Especificacion;

namespace SGPE.WebApi.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SGPEContext _db;
        private readonly IMapper _mapper;
        private readonly IContextAccessor _contextAccessor;

        public UsuarioService(SGPEContext db, IMapper mapper, IContextAccessor contextAccessor)
        {
            _db = db;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<PagedResult<UsuarioDto>> GetUsuarios(GetEntityQuery query)
        {
            var especificacion = new UsuarioEspecificacion(query.SearchText ?? "");

            return await _db.Usuarios
                .Where(especificacion.Criteria)
                .ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider)
                .GetPagedResultAsync(query.PageSize, query.CurrentPage);
        }

        public async Task<UsuarioEditDto> GetUsuario(Guid idUsuario)
        {
            return await _db.Usuarios
                .Include(u => u.IdEmpresaNavigation)
                .Where(u => u.IdUsuario == idUsuario)
                .ProjectTo<UsuarioEditDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Usuario), idUsuario);
        }

        public async Task<string> CreateUsuario(UsuarioEditDto usuarioDto)
        {
            if (_db.Usuarios.Any(u => u.CedulaUsuario.Trim() == usuarioDto.CedulaUsuario.Trim()))
                throw new ValidationException("La cedula ya esta registrada");

            if (!string.IsNullOrEmpty(usuarioDto.Correo) && await EmailExists(usuarioDto.Correo))
                throw new ValidationException("El correo ya esta registrado");

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            var hash = HashHelper.Hash(usuario.CedulaUsuario);

            usuario.IdUsuario = Guid.NewGuid();
            usuario.PasswordHash = hash.Password;
            usuario.PasswordSalt = hash.Salt;
            usuario.CreaUsuario = _contextAccessor.UserName;
            usuario.CreaMaquina = _contextAccessor.ClientIP;
            usuario.CreaFecha = DateTime.Now;

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return "¡Usuario creado!";
        }

        public async Task<string> UpdateUsuario(UsuarioEditDto usuarioDto)
        {
            var updateUsuario = await GetUsuarioById(usuarioDto.IdUsuario);

            var usuarioPerfiles = _mapper.Map<List<UsuarioPerfil>>(usuarioDto.UsuarioPerfils);
            await _db.UsuarioPerfils.Where(u => u.IdUsuario == usuarioDto.IdUsuario).ExecuteDeleteAsync();

            updateUsuario.IdEmpresa = usuarioDto.IdEmpresa;
            updateUsuario.CedulaUsuario = usuarioDto.CedulaUsuario;
            updateUsuario.Nombres = usuarioDto.Nombres;
            updateUsuario.Apellidos = usuarioDto.Apellidos;
            updateUsuario.Correo = usuarioDto.Correo;
            updateUsuario.ModificaUsuario = _contextAccessor.UserName;
            updateUsuario.ModificaMaquina = _contextAccessor.ClientIP;
            updateUsuario.ModificaFecha = DateTime.Now;
            updateUsuario.UsuarioPerfils = usuarioPerfiles;

            await _db.SaveChangesAsync();

            return "¡Usuario actualizado!";
        }

        public async Task<string> ChangeStatusUsuario(Guid id)
        {
            var usuario = await GetUsuarioById(id);
            usuario.Activo = !usuario.Activo;

            await _db.SaveChangesAsync();

            return $"¡Usuario {(usuario.Activo ? "activado" : "inactivado")} correctamente!";
        }

        public async Task<Usuario> GetUsuarioById(Guid idUsuario)
        {
            return await _db.Usuarios.FindAsync(idUsuario)
                ?? throw new NotFoundException(nameof(Usuario), idUsuario);
        }

        public async Task<Usuario> GetUsuarioByCedula(string cedula)
        {
            return await _db.Usuarios.FirstOrDefaultAsync(u => u.CedulaUsuario == cedula)
                ?? throw new NotFoundException("La cedula no existe");
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _db.Usuarios.FirstOrDefaultAsync(u => u.Correo!.Trim().ToLower() == email.Trim().ToLower())
                ?? throw new NotFoundException("El correo electrónico no es valido");
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _db.Usuarios.AnyAsync(u => u.Correo!.Trim().ToLower() == email.Trim().ToLower());
        }
    }
}
