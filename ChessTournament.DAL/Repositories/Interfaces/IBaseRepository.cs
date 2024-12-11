namespace ChessTournament.DAL.Repositories.Interface
{
    public interface IBaseRepository<T> 
    {
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
    }
}
