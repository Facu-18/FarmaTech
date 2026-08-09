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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpleadaDTO>> GetById(int id) { 
            var empleada = await context.Empleadas.FirstOrDefaultAsync(e => e.Id == id);
            if(empleada == null)
            {
                return NotFound($"No se encontro la empleada de id: {id}");
            }
            EmpleadaDTO dto= new EmpleadaDTO();
            dto.Nombre=empleada.Nombre;
            dto.Usuario=empleada.Usuario;
            dto.Pin=empleada.Pin;

            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> Put(int id, EmpleadaDTO empleadaDTO)
        {
            var empleada = await context.Empleadas
                .FirstOrDefaultAsync(e => e.Id == id);

            if (empleada == null)
            {
                return NotFound($"No se encontró la empleada con id: {id}");
            }

            empleada.Nombre = empleadaDTO.Nombre;
            empleada.Apellido = empleadaDTO.Apellido;
            empleada.Usuario = empleadaDTO.Usuario;
            empleada.Pin = empleadaDTO.Pin;

            await context.SaveChangesAsync();

            return Ok(empleada.Nombre);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id) {
            var empleada = await context.Empleadas
                    .FirstOrDefaultAsync(e => e.Id == id);

            if (empleada == null)
            {
                return NotFound($"No se encontró la empleada con id: {id}");
            }

            context.Empleadas.Remove(empleada);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
