using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/venta")]
    public class VentaController : Controller
    {
        private readonly IRepositorio<Venta> repositorio;

        public VentaController(IRepositorio<Venta> repositorio) {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Venta>>> Get()
        {
            var ventas = await repositorio.Select();
            return Ok(ventas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VentaDTO>> GetById(int id) {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No se encontro la venta de id: {id}");
            }

            var venta = await repositorio.SelectById(id);
            VentaDTO dto = new VentaDTO();
            dto.IdEmpleada = venta!.IdEmpleada;
            dto.Fecha = venta.Fecha;
            dto.Total = venta.Total;

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(VentaDTO ventaDTO)
        {
            Venta venta = new Venta();
            venta.IdEmpleada = ventaDTO.IdEmpleada;
            venta.Fecha = ventaDTO.Fecha;
            venta.Total = ventaDTO.Total;

            await repositorio.Insert(venta);

            return Ok(venta.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Put(int id, VentaDTO ventaDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            var venta = await repositorio.SelectById(id);
            venta!.IdEmpleada = ventaDTO.IdEmpleada;
            venta.Fecha = ventaDTO.Fecha;
            venta.Total = ventaDTO.Total;

            var resultado = await repositorio.Update(venta);

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
