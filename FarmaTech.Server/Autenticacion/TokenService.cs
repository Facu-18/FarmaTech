using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FarmaTech.BD.Datos.Entity;
using FarmaTech.Shared.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FarmaTech.Server.Autenticacion
{
    public sealed class TokenService : ITokenService
    {
        private readonly JwtOptions options;

        public TokenService(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }

        public LoginResponseDTO CrearToken(
            Empleada empleada,
            IReadOnlyCollection<string> roles)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(options.ExpirationMinutes);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, empleada.Id.ToString()),
                new(ClaimTypes.NameIdentifier, empleada.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, empleada.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expira = expiration,
                Id = empleada.Id,
                Rol = roles.FirstOrDefault() ?? string.Empty
            };
        }
    }
}
