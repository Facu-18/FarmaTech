using FarmaTech.BD.Datos.Entity;
using Microsoft.EntityFrameworkCore;


namespace FarmaTech.BD.Datos
{
    public class AppDbContext : DbContext
    {
        public DbSet<DetalleVenta> DetalleVentas { get; set; } = null!;
        public DbSet<Drogueria> Droguerias { get; set; } = null!;
        public DbSet<Empleada> Empleadas { get; set; } = null!;
        public DbSet<IngresoMercaderia> IngresosMercaderia { get; set; } = null!;
        public DbSet<NotaPedido> NotasPedido { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Venta> Ventas { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

    }
}
