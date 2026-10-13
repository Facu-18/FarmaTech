using FarmaTech.BD.Datos.Entity;

namespace FarmaTech.Repository.Repositorios
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        Task<Producto?> SelectByCodigo(string codigo);
        Task<List<Producto>> Buscar(string texto, bool incluirInactivos = false);
    }
}
