using FarmaTech.BD.Datos;

namespace FarmaTech.Repository.Repositorios
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        Task<bool> Delete(int id);
        Task<E> Insert(E entity);
        Task<List<E>> Select();
        Task<E?> SelectById(int id);
        Task<bool> Update(E entity);
    }
}