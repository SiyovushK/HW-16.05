namespace Infrastructure.Interfaces;

public interface IBaseRepository<T1, T2>
{
    Task<IQueryable<T1>> GetAllAsync();
    Task<T1?> GetByIdAsync(int id);
    Task<T2?> AddAsync(T1 entity);
    Task<T2?> UpdateAsync(T1 entity);
    Task<T2?> DeleteAsync(T1 entity);
}