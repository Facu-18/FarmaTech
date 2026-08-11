using FarmaTech.BD.Datos;
using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/empleada")]
    public class EmpleadaController : Controller
    {
        private readonly IEmpleadaRepositorio repositorio;

        public EmpleadaController(IEmpleadaRepositorio repositorio) {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Empleada>>> Get()
        {
            var empleadas = await repositorio.Select();
            return Ok(empleadas);
        }

        [HttpGet("usuario-pin")]
        public async Task<ActionResult<List<Empleada>>> GetWithPin()
        {
            var empleadas = await repositorio.ObtenerUsuariosConPin();
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

            await repositorio.Insert(empleada);
            
            return Ok(empleada.Nombre);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpleadaDTO>> GetById(int id) {
            var empleada = await repositorio.SelectById(id);
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
        public async Task<ActionResult<bool>> Put(int id, EmpleadaDTO empleadaDTO)
        {
            var empleada = await repositorio.SelectById(id);
            if (empleada == null)
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            empleada.Nombre = empleadaDTO.Nombre;
            empleada.Apellido = empleadaDTO.Apellido;
            empleada.Usuario = empleadaDTO.Usuario;
            empleada.Pin = empleadaDTO.Pin;

            var resultado = await repositorio.Update(empleada);

            return Ok(resultado);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id) {

            var resultado = await repositorio.Delete(id);
            if (!resultado)
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            return Ok(true);
        }
    }
}
