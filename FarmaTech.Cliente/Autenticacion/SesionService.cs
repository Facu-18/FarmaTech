using System.Text.Json;
using FarmaTech.Servicios.ServiciosHTTP;
using FarmaTech.Shared.DTO;
using Microsoft.JSInterop;

namespace FarmaTech.Cliente.Autenticacion
{
    public class SesionService
    {
        private const string ClaveAlmacenamiento = "farmatech.sesion";

        private readonly IJSRuntime js;
        private readonly IHttpService http;
        private Task? restauracion;

        public SesionService(IJSRuntime js, IHttpService http)
        {
            this.js = js;
            this.http = http;
        }

        public LoginResponseDTO? Sesion { get; private set; }
        public string? Usuario { get; private set; }

        public bool EstaVigente => Sesion is not null && Sesion.Expira > DateTime.UtcNow;

        public event Action? SesionCambiada;

        /// <summary>Loads the stored session once; later calls reuse the same task.</summary>
        public Task RestaurarAsync() => restauracion ??= RestaurarInternoAsync();

        public async Task<HttpResp<LoginResponseDTO?>> LoginAsync(LoginDTO dto)
        {
            var resp = await http.PostAsync<LoginDTO, LoginResponseDTO>("api/auth/login", dto);
            if (!resp.Error && resp.Respuesta is not null)
            {
                await EstablecerSesionAsync(resp.Respuesta, dto.Usuario.Trim());
            }

            return resp;
        }

        public async Task LogoutAsync()
        {
            Sesion = null;
            Usuario = null;
            http.EstablecerToken(null);
            await EliminarAlmacenadaAsync();
            SesionCambiada?.Invoke();
        }

        private async Task EstablecerSesionAsync(LoginResponseDTO sesion, string usuario)
        {
            Sesion = sesion;
            Usuario = usuario;
            http.EstablecerToken(sesion.Token);

            var json = JsonSerializer.Serialize(new SesionAlmacenada { Datos = sesion, Usuario = usuario });
            try
            {
                await js.InvokeVoidAsync("localStorage.setItem", ClaveAlmacenamiento, json);
            }
            catch (JSException)
            {
                // Storage unavailable: the session still works until the page is reloaded.
            }

            SesionCambiada?.Invoke();
        }

        private async Task RestaurarInternoAsync()
        {
            try
            {
                var json = await js.InvokeAsync<string?>("localStorage.getItem", ClaveAlmacenamiento);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return;
                }

                var guardada = JsonSerializer.Deserialize<SesionAlmacenada>(json);
                if (guardada?.Datos is null ||
                    string.IsNullOrEmpty(guardada.Datos.Token) ||
                    guardada.Datos.Expira <= DateTime.UtcNow)
                {
                    await EliminarAlmacenadaAsync();
                    return;
                }

                Sesion = guardada.Datos;
                Usuario = guardada.Usuario;
                http.EstablecerToken(Sesion.Token);
            }
            catch (Exception e) when (e is JSException or JsonException)
            {
                await EliminarAlmacenadaAsync();
            }
        }

        private async Task EliminarAlmacenadaAsync()
        {
            try
            {
                await js.InvokeVoidAsync("localStorage.removeItem", ClaveAlmacenamiento);
            }
            catch (JSException)
            {
            }
        }

        private class SesionAlmacenada
        {
            public LoginResponseDTO? Datos { get; set; }
            public string? Usuario { get; set; }
        }
    }
}
