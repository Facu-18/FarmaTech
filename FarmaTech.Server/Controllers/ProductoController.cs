using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Server.Autenticacion;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/producto")]
    public class ProductoController : Controller
    {
        private readonly IProductoRepositorio repositorio;

        public ProductoController(IProductoRepositorio repositorio) {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            var productos = await repositorio.Select();
            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoResponseDTO>> GetById(int id) {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No se encontro el producto de id: {id}");
            }

            var producto = await repositorio.SelectById(id);

            return Ok(ToResponse(producto!));
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<ProductoResponseDTO>> GetByCodigo(string codigo)
        {
            var producto = await repositorio.SelectByCodigo(codigo);
            if (producto is null)
            {
                return NotFound($"No se encontro el producto con codigo: {codigo}");
            }

            return Ok(ToResponse(producto));
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<List<ProductoResponseDTO>>> Buscar([FromQuery] string? texto, [FromQuery] bool incluirInactivos = false)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return BadRequest("Debe ingresar un texto de busqueda.");
            }

            var productos = await repositorio.Buscar(texto, incluirInactivos);
            return Ok(productos.Select(ToResponse).ToList());
        }

        private static ProductoResponseDTO ToResponse(Producto producto)
        {
            ProductoResponseDTO dto = new ProductoResponseDTO();
            dto.Id = producto.Id;
            dto.Codigo = producto.Codigo;
            dto.Nombre = producto.Nombre;
            dto.Descripcion = producto.Descripcion;
            dto.Categoria = producto.Categoria;
            dto.Precio = producto.Precio;
            dto.Estado = producto.Estado;
            return dto;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Encargada)]
        public async Task<ActionResult<int>> Post(ProductoDTO productoDTO)
        {
            if (await repositorio.SelectByCodigo(productoDTO.Codigo) is not null)
            {
                return BadRequest($"Ya existe un producto con el codigo: {productoDTO.Codigo}");
            }

            Producto producto = new Producto();
            producto.Codigo = productoDTO.Codigo;
            producto.Nombre = productoDTO.Nombre;
            producto.Descripcion = productoDTO.Descripcion;
            producto.Categoria = productoDTO.Categoria;
            producto.Precio = productoDTO.Precio;
            producto.Estado = productoDTO.Estado;

            await repositorio.Insert(producto);

            return Ok(producto.Id);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Roles.Encargada)]
        public async Task<ActionResult<bool>> Put(int id, ProductoDTO productoDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            var otro = await repositorio.SelectByCodigo(productoDTO.Codigo);
            if (otro is not null && otro.Id != id)
            {
                return BadRequest($"Ya existe un producto con el codigo: {productoDTO.Codigo}");
            }

            var producto = await repositorio.SelectById(id);
            producto!.Codigo = productoDTO.Codigo;
            producto.Nombre = productoDTO.Nombre;
            producto.Descripcion = productoDTO.Descripcion;
            producto.Categoria = productoDTO.Categoria;
            producto.Precio = productoDTO.Precio;
            producto.Estado = productoDTO.Estado;

            var resultado = await repositorio.Update(producto);

            return Ok(resultado);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Encargada)]
        public async Task<ActionResult<bool>> Delete(int id) {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No existe el registro con id: {id}");
            }

            // Logical delete: keep the row so sale history (detalle_venta) stays intact
            var producto = await repositorio.SelectById(id);
            producto!.Estado = "Inactivo";
            await repositorio.Update(producto);

            return Ok(true);
        }
    }
}
