using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace SGPE.Comun.JWT;

public class JWTFactory : IJWTFactory
{
    private readonly JWTOptions _jwtOptions;

    public JWTFactory(IOptions<JWTOptions> jwtoptions)
    {
        _jwtOptions = jwtoptions.Value;

        if (_jwtOptions is null) throw new ArgumentNullException(nameof(jwtoptions));
        if (_jwtOptions.ValidForMinutes <= 0) throw new ArgumentException("Debe ser mayor a cero");
        if (_jwtOptions.SigningCredentials is null) throw new ArgumentNullException(nameof(_jwtOptions.SigningCredentials));
    }

    public string GenerateEncodedToken(string userName, ClaimsIdentity identity, string refreshToken)
    {
        var expiration = _jwtOptions.Expiration;

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: identity.Claims,
            notBefore: _jwtOptions.NotBefore,
            expires: expiration,
            signingCredentials: _jwtOptions.SigningCredentials);

        return JsonSerializer.Serialize(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration,
            refreshToken
        });
    }

    public JwtSecurityToken DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        return jsonToken ?? new JwtSecurityToken();
    }
}
