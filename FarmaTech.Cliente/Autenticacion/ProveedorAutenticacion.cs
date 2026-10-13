using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace FarmaTech.Cliente.Autenticacion
{
    public class ProveedorAutenticacion : AuthenticationStateProvider, IDisposable
    {
        private static readonly AuthenticationState Anonimo = new(new ClaimsPrincipal(new ClaimsIdentity()));

        private readonly SesionService sesionService;

        public ProveedorAutenticacion(SesionService sesionService)
        {
            this.sesionService = sesionService;
            sesionService.SesionCambiada += AlCambiarSesion;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await sesionService.RestaurarAsync();

            var sesion = sesionService.Sesion;
            if (!sesionService.EstaVigente || sesion is null)
            {
                return Anonimo;
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, sesion.Id.ToString()),
                new(ClaimTypes.Name, sesionService.Usuario ?? sesion.Id.ToString())
            };

            IEnumerable<string> roles = sesion.Roles.Count > 0 ? sesion.Roles : new[] { sesion.Rol };
            claims.AddRange(roles
                .Where(rol => !string.IsNullOrEmpty(rol))
                .Select(rol => new Claim(ClaimTypes.Role, rol)));

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        private void AlCambiarSesion() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        public void Dispose() => sesionService.SesionCambiada -= AlCambiarSesion;
    }
}
