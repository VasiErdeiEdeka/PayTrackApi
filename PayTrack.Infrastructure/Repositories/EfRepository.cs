using Microsoft.EntityFrameworkCore;
using PayTrack.Application;
using PayTrack.Infrastructure.Context;
using System.Linq.Expressions;

namespace PayTrack.Infrastructure.Repositories;

public class EfRepository<T>(PayTrackDbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T> GetByIdAsync(object id) => await _dbSet.FindAsync(id) ?? throw new CustomException("No DB element found with the requested id!");

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}