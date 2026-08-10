using FarmaTech.BD.Datos;
using FarmaTech.BD.Datos.Entity;
using FarmaTech.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace FarmaTech.Repository.Repositorios
{
    public class EmpleadaRepositorio : Repositorio<Empleada>, IEmpleadaRepositorio
    {
        private readonly AppDbContext context;
        public EmpleadaRepositorio(AppDbContext context) : base(context) 
        {
            this.context = context;
        }

        public async Task<List<DatoEmpleadaDTO>> ObtenerUsuariosConPin()
        {
            var lista = await
            context.Empleadas
                        .Select(p => new DatoEmpleadaDTO
                        {
                            Id = p.Id,
                            DatosEmpleada = $"{p.Usuario} - {p.Pin}"
                        })
                        .ToListAsync();
                        
            return lista;
        }
    }
}
