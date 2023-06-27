namespace Instagram.Application.Interfaces;

public interface IGenericRepository<T, TId>
    where T : class
    where TId : class
{
    Task AddAsync(T entity);
    IQueryable<T> GetAll();
    Task<T?> GetById(TId id);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}