using FarmaTech.BD.Datos.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FarmaTech.BD.Datos
{
    public class AppDbContext : IdentityDbContext<Empleada, IdentityRole<int>, int>
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Empleada>().ToTable("Empleadas");
        }

    }
}
