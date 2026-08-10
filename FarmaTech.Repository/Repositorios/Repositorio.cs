using FarmaTech.BD.Datos;
using Microsoft.EntityFrameworkCore;

namespace FarmaTech.Repository.Repositorios
{
    public class Repositorio<E> : IRepositorio<E> where E : class
    {

        private readonly AppDbContext context;
        public Repositorio(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<E>> Select()
        {
            return await context.Set<E>().ToListAsync();
        }
    }
}
