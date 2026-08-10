using FarmaTech.BD.Datos.Entity;
using FarmaTech.Shared.DTO;

namespace FarmaTech.Repository.Repositorios
{
    public interface IEmpleadaRepositorio : IRepositorio<Empleada>
    {
        Task<List<DatoEmpleadaDTO>> ObtenerUsuariosConPin();
    }
}
