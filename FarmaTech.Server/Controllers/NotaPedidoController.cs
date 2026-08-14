using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/nota-pedido")]
    public class NotaPedidoController : Controller
    {
        private readonly IRepositorio<NotaPedido> repositorio;

        public NotaPedidoController(IRepositorio<NotaPedido> repositorio) {
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
