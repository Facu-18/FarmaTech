namespace FarmaTech.Repository.Repositorios
{
    public interface IRepositorio<E> where E : class
    {
        Task<List<E>> Select();
    }
}
