using FarmaTech.BD.Datos;
using FarmaTech.BD.Datos.Entity;

namespace FarmaTech.Repository.Repositorios
{
    public class EmpleadaRepositorio : Repositorio<Empleada>, IEmpleadaRepositorio
    {
        public EmpleadaRepositorio(AppDbContext context) : base(context) 
        {
        }
    }
}
