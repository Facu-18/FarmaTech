using FarmaTech.BD.Datos;
using FarmaTech.BD.Datos.Entity;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/empleada")]
    public class EmpleadaController : Controller
    {
        private readonly AppDbContext context;

        public EmpleadaController(AppDbContext context) { 
        
            this.context = context;
        
        }

        [HttpGet]
        public async Task<ActionResult<List<Empleada>>> Get()
        {
            var empleadas = await context.Empleadas.ToListAsync();
            return Ok(empleadas);
        }

        [HttpPost]
        public async Task <ActionResult<string>> Post(EmpleadaDTO empleadaDTO)
        {
            Empleada empleada = new Empleada();
            empleada.Nombre = empleadaDTO.Nombre;
            empleada.Apellido = empleadaDTO.Apellido;
            empleada.Usuario = empleadaDTO.Usuario;
            empleada.Pin = empleadaDTO.Pin;
            context.Empleadas.Add(empleada);
            await context.SaveChangesAsync();
            return Ok(empleada.Nombre);
        }
    }
}
