using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/drogueria")]
    public class DrogueriaController : Controller
    {
        private readonly IRepositorio<Drogueria> repositorio;

        public DrogueriaController(IRepositorio<Drogueria> repositorio) {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Drogueria>>> Get()
        {
            var droguerias = await repositorio.Select();
            return Ok(droguerias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DrogueriaDTO>> GetById(int id) {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No se encontro la drogueria de id: {id}");
            }

            var drogueria = await repositorio.SelectById(id);
            DrogueriaDTO dto = new DrogueriaDTO();
            dto.Nombre = drogueria!.Nombre;
            dto.CanalContacto = drogueria.CanalContacto;
            dto.Contacto = drogueria.Contacto;

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(DrogueriaDTO drogueriaDTO)
        {
            Drogueria drogueria = new Drogueria();
            drogueria.Nombre = drogueriaDTO.Nombre;
            drogueria.CanalContacto = drogueriaDTO.CanalContacto;
            drogueria.Contacto = drogueriaDTO.Contacto;

            await repositorio.Insert(drogueria);

            return Ok(drogueria.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Put(int id, DrogueriaDTO drogueriaDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            var drogueria = await repositorio.SelectById(id);
            drogueria!.Nombre = drogueriaDTO.Nombre;
            drogueria.CanalContacto = drogueriaDTO.CanalContacto;
            drogueria.Contacto = drogueriaDTO.Contacto;

            var resultado = await repositorio.Update(drogueria);

            return Ok(resultado);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id) {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            await repositorio.Delete(id);

            return Ok(true);
        }
    }
}
