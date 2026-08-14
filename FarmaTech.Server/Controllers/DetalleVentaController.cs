using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/detalle-venta")]
    public class DetalleVentaController : Controller
    {
        private readonly IRepositorio<DetalleVenta> repositorio;

        public DetalleVentaController(IRepositorio<DetalleVenta> repositorio) {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<DetalleVenta>>> Get()
        {
            var detalles = await repositorio.Select();
            return Ok(detalles);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DetalleVentaDTO>> GetById(int id) {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No se encontro el detalle de venta de id: {id}");
            }

            var detalle = await repositorio.SelectById(id);
            DetalleVentaDTO dto = new DetalleVentaDTO();
            dto.IdVenta = detalle!.IdVenta;
            dto.IdProducto = detalle.IdProducto;
            dto.Cantidad = detalle.Cantidad;
            dto.PrecioUnitario = detalle.PrecioUnitario;
            dto.Subtotal = detalle.Subtotal;

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(DetalleVentaDTO detalleVentaDTO)
        {
            DetalleVenta detalleVenta = new DetalleVenta();
            detalleVenta.IdVenta = detalleVentaDTO.IdVenta;
            detalleVenta.IdProducto = detalleVentaDTO.IdProducto;
            detalleVenta.Cantidad = detalleVentaDTO.Cantidad;
            detalleVenta.PrecioUnitario = detalleVentaDTO.PrecioUnitario;
            detalleVenta.Subtotal = detalleVentaDTO.Subtotal;

            await repositorio.Insert(detalleVenta);

            return Ok(detalleVenta.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Put(int id, DetalleVentaDTO detalleVentaDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            var detalleVenta = await repositorio.SelectById(id);
            detalleVenta!.IdVenta = detalleVentaDTO.IdVenta;
            detalleVenta.IdProducto = detalleVentaDTO.IdProducto;
            detalleVenta.Cantidad = detalleVentaDTO.Cantidad;
            detalleVenta.PrecioUnitario = detalleVentaDTO.PrecioUnitario;
            detalleVenta.Subtotal = detalleVentaDTO.Subtotal;

            var resultado = await repositorio.Update(detalleVenta);

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
