using Instagram.Application.Interfaces;
using Instagram.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Repositories;



public class GenericRepository<T, TId> 
    : IGenericRepository<T, TId>
    where T : class
    where TId : class
{
    protected readonly InstagramDbContext _context;
    private readonly DbSet<T> _entities;

    public GenericRepository(
        InstagramDbContext context
        )
    {
        _context = context;
        _entities = _context.Set<T>();
    }


    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public virtual void Delete(T entity)
    {
        _entities.Remove(entity);
    }

    public IQueryable<T> GetAll()
    {
        return _entities.AsQueryable();
    }

    public virtual async Task<T?> GetById(TId id)
    {
        return await _entities.FindAsync(id);
    }

    public virtual void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }


}