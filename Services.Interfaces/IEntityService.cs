using Models;

namespace Services.Interfaces
{
    public interface IEntityService<T> where T : Entity
    {
        Task<T?> ReadAsync(int id);
        Task<IEnumerable<T>> ReadAsync();
        Task<int> CreateAsync(T entity);

        Task DeleteAsync(int id);
    }
}