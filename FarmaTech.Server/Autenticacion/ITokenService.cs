using FarmaTech.BD.Datos.Entity;
using FarmaTech.Shared.DTO;

namespace FarmaTech.Server.Autenticacion
{
    public interface ITokenService
    {
        LoginResponseDTO CrearToken(Empleada empleada, IReadOnlyCollection<string> roles);
    }
}
