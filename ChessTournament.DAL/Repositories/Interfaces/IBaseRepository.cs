namespace ChessTournament.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T> 
    {
        Task<T?> CreateAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
    }
}
