using Microsoft.EntityFrameworkCore;


namespace FarmaTech.BD.Datos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}