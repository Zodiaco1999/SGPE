using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SGPE.Comun.JWT;

public interface IJWTFactory
{
    string GenerateEncodedToken(string userName, ClaimsIdentity identity, string refreshToken);
    JwtSecurityToken DecodeToken(string token);
}