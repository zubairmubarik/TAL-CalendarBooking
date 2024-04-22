namespace Application.Common.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T?> CreateAsync(T entity, CancellationToken cancellationToken);
        Task<T?> UpdateAsync(Guid id, T entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<T?> PatchAsync(Guid id, T entity, CancellationToken cancellationToken);           
        Task<IList<T>?> GetAsync(CancellationToken cancellationToken);
        Task<T?> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> IsExist(Guid id, CancellationToken cancellationToken);
    }
}
