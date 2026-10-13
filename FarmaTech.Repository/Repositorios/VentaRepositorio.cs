using FarmaTech.BD.Datos;
using FarmaTech.BD.Datos.Entity;
using Microsoft.EntityFrameworkCore;

namespace FarmaTech.Repository.Repositorios
{
    public class VentaRepositorio : Repositorio<Venta>, IVentaRepositorio
    {
        public VentaRepositorio(AppDbContext context) : base(context)
        {
        }

        public async Task<(Venta? Venta, int? IdProductoInexistente)> Registrar(
            int idEmpleada,
            IEnumerable<(int IdProducto, int Cantidad)> items)
        {
            Dictionary<int, int> cantidades = items
                .GroupBy(i => i.IdProducto)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.Cantidad));

            List<int> ids = cantidades.Keys.ToList();
            Dictionary<int, Producto> productos = await context.Set<Producto>()
                .Where(p => ids.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            foreach (int id in ids)
            {
                if (!productos.ContainsKey(id))
                {
                    return (null, id);
                }
            }

            Venta venta = new Venta();
            venta.IdEmpleada = idEmpleada;
            venta.Fecha = DateTime.Now;

            foreach (KeyValuePair<int, int> item in cantidades)
            {
                Producto producto = productos[item.Key];
                DetalleVenta detalle = new DetalleVenta();
                detalle.Producto = producto;
                detalle.Cantidad = item.Value;
                detalle.PrecioUnitario = producto.Precio;
                detalle.Subtotal = producto.Precio * item.Value;
                venta.Detalles.Add(detalle);
            }

            venta.Total = venta.Detalles.Sum(d => d.Subtotal);

            await context.Set<Venta>().AddAsync(venta);
            await context.SaveChangesAsync();

            return (venta, null);
        }
    }
}
