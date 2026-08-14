using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/ingreso-mercaderia")]
    public class IngresoMercaderiaController : Controller
    {
        private readonly IRepositorio<IngresoMercaderia> repositorio;

        public IngresoMercaderiaController(IRepositorio<IngresoMercaderia> repositorio) {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<IngresoMercaderia>>> Get()
        {
            var ingresos = await repositorio.Select();
            return Ok(ingresos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IngresoMercaderiaDTO>> GetById(int id) {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No se encontro el ingreso de mercaderia de id: {id}");
            }

            var ingreso = await repositorio.SelectById(id);
            IngresoMercaderiaDTO dto = new IngresoMercaderiaDTO();
            dto.Fecha = ingreso!.Fecha;
            dto.Cantidad = ingreso.Cantidad;
            dto.IdProducto = ingreso.IdProducto;
            dto.IdDrogueria = ingreso.IdDrogueria;
            dto.IdEmpleada = ingreso.IdEmpleada;

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(IngresoMercaderiaDTO ingresoMercaderiaDTO)
        {
            IngresoMercaderia ingresoMercaderia = new IngresoMercaderia();
            ingresoMercaderia.Fecha = ingresoMercaderiaDTO.Fecha;
            ingresoMercaderia.Cantidad = ingresoMercaderiaDTO.Cantidad;
            ingresoMercaderia.IdProducto = ingresoMercaderiaDTO.IdProducto;
            ingresoMercaderia.IdDrogueria = ingresoMercaderiaDTO.IdDrogueria;
            ingresoMercaderia.IdEmpleada = ingresoMercaderiaDTO.IdEmpleada;

            await repositorio.Insert(ingresoMercaderia);

            return Ok(ingresoMercaderia.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Put(int id, IngresoMercaderiaDTO ingresoMercaderiaDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            var ingresoMercaderia = await repositorio.SelectById(id);
            ingresoMercaderia!.Fecha = ingresoMercaderiaDTO.Fecha;
            ingresoMercaderia.Cantidad = ingresoMercaderiaDTO.Cantidad;
            ingresoMercaderia.IdProducto = ingresoMercaderiaDTO.IdProducto;
            ingresoMercaderia.IdDrogueria = ingresoMercaderiaDTO.IdDrogueria;
            ingresoMercaderia.IdEmpleada = ingresoMercaderiaDTO.IdEmpleada;

            var resultado = await repositorio.Update(ingresoMercaderia);

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
