using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoController : Controller
    {
        private readonly IRepositorio<Producto> repositorio;

        public ProductoController(IRepositorio<Producto> repositorio) {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            var productos = await repositorio.Select();
            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id) {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No se encontro el producto de id: {id}");
            }

            var producto = await repositorio.SelectById(id);
            ProductoDTO dto = new ProductoDTO();
            dto.Codigo = producto!.Codigo;
            dto.Nombre = producto.Nombre;
            dto.Descripcion = producto.Descripcion;
            dto.Categoria = producto.Categoria;
            dto.Precio = producto.Precio;
            dto.Estado = producto.Estado;

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(ProductoDTO productoDTO)
        {
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
        public async Task<ActionResult<bool>> Put(int id, ProductoDTO productoDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"No existe el registro con id: {id}");
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
