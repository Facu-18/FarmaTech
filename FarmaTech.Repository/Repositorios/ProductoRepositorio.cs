using FarmaTech.BD.Datos;
using FarmaTech.BD.Datos.Entity;
using Microsoft.EntityFrameworkCore;

namespace FarmaTech.Repository.Repositorios
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        public ProductoRepositorio(AppDbContext context) : base(context)
        {
        }

        public async Task<Producto?> SelectByCodigo(string codigo)
        {
            string valor = codigo.Trim();
            return await context.Set<Producto>().FirstOrDefaultAsync(p => p.Codigo == valor);
        }

        public async Task<List<Producto>> Buscar(string texto, bool incluirInactivos = false)
        {
            string valor = texto.Trim();
            // Any estado other than "Inactivo" counts as active (legacy rows)
            return await context.Set<Producto>()
                .Where(p => p.Nombre.Contains(valor) || p.Codigo.Contains(valor))
                .Where(p => incluirInactivos || p.Estado != "Inactivo")
                .OrderBy(p => p.Nombre)
                .Take(50)
                .ToListAsync();
        }
    }
}
