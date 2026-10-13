using FarmaTech.BD.Datos.Entity;

namespace FarmaTech.Repository.Repositorios
{
    public interface IVentaRepositorio : IRepositorio<Venta>
    {
        // Returns the saved sale (with details and products), or null plus the first missing product id.
        Task<(Venta? Venta, int? IdProductoInexistente)> Registrar(
            int idEmpleada,
            IEnumerable<(int IdProducto, int Cantidad)> items);
    }
}
