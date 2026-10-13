using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/nota-pedido")]
    public class NotaPedidoController : Controller
    {
        private readonly IRepositorio<NotaPedido> repositorio;
        private readonly IRepositorio<Producto> productos;
        private readonly IRepositorio<Drogueria> droguerias;

        public NotaPedidoController(
            IRepositorio<NotaPedido> repositorio,
            IRepositorio<Producto> productos,
            IRepositorio<Drogueria> droguerias) {
            this.productos = productos;
            this.droguerias = droguerias;
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotaPedido>>> Get()
        {
            var notasPedido = await repositorio.Select();
            return Ok(notasPedido);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<NotaPedidoDTO>> GetById(int id) {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No se encontro la nota de pedido de id: {id}");
            }

            var notaPedido = await repositorio.SelectById(id);
            NotaPedidoDTO dto = new NotaPedidoDTO();
            dto.Fecha = notaPedido!.Fecha;
            dto.Cantidad = notaPedido.Cantidad;
            dto.Estado = notaPedido.Estado;
            dto.IdProducto = notaPedido.IdProducto;
            dto.IdDrogueria = notaPedido.IdDrogueria;
            dto.IdEmpleada = notaPedido.IdEmpleada;

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(NotaPedidoDTO notaPedidoDTO)
        {
            NotaPedido notaPedido = new NotaPedido();
            notaPedido.Fecha = notaPedidoDTO.Fecha;
            notaPedido.Cantidad = notaPedidoDTO.Cantidad;
            notaPedido.Estado = notaPedidoDTO.Estado;
            notaPedido.IdProducto = notaPedidoDTO.IdProducto;
            notaPedido.IdDrogueria = notaPedidoDTO.IdDrogueria;
            notaPedido.IdEmpleada = notaPedidoDTO.IdEmpleada;

            await repositorio.Insert(notaPedido);

            return Ok(notaPedido.Id);
        }

        [Authorize]
        [HttpPost("faltante")]
        public async Task<ActionResult<int>> Faltante(FaltanteDTO faltanteDTO)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idEmpleada))
            {
                return Unauthorized();
            }

            if (!await productos.Existe(faltanteDTO.IdProducto))
            {
                return NotFound($"No se encontro el producto de id: {faltanteDTO.IdProducto}");
            }

            if (faltanteDTO.IdDrogueria.HasValue && !await droguerias.Existe(faltanteDTO.IdDrogueria.Value))
            {
                return NotFound($"No se encontro la drogueria de id: {faltanteDTO.IdDrogueria}");
            }

            NotaPedido notaPedido = new NotaPedido();
            notaPedido.Fecha = DateTime.Now;
            notaPedido.Cantidad = faltanteDTO.Cantidad;
            notaPedido.Estado = "Pendiente";
            notaPedido.IdProducto = faltanteDTO.IdProducto;
            notaPedido.IdDrogueria = faltanteDTO.IdDrogueria;
            notaPedido.IdEmpleada = idEmpleada;

            await repositorio.Insert(notaPedido);

            return Ok(notaPedido.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Put(int id, NotaPedidoDTO notaPedidoDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            var notaPedido = await repositorio.SelectById(id);
            notaPedido!.Fecha = notaPedidoDTO.Fecha;
            notaPedido.Cantidad = notaPedidoDTO.Cantidad;
            notaPedido.Estado = notaPedidoDTO.Estado;
            notaPedido.IdProducto = notaPedidoDTO.IdProducto;
            notaPedido.IdDrogueria = notaPedidoDTO.IdDrogueria;
            notaPedido.IdEmpleada = notaPedidoDTO.IdEmpleada;

            var resultado = await repositorio.Update(notaPedido);

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
