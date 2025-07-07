﻿using System.Linq.Expressions;

namespace PayTrack.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(object id);

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task SaveChangesAsync();
}