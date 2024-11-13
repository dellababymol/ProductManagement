using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Database.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;
    private DbSet<TEntity> _entities;

    public Repository(DbContext context)
    {
        Context = context;
        _entities = context.Set<TEntity>();
    }

    public TEntity Get(Guid id)
    {
        try
        {
            return _entities.Find(id);
        }
        catch
        {
            throw new ArgumentNullException();
        }
    }

    public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return _entities.FirstOrDefault(predicate);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _entities.AsEnumerable();
    }

    public void Update(TEntity entity)
    {
        Context.Update(entity);
        Context.SaveChanges();
    }

    public void Add(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        _entities.Add(entity);
        Context.SaveChanges();
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _entities.AddRange(entities);
        Context.SaveChanges();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _entities.Where(predicate);
    }

    public void Remove(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        _entities.Remove(entity);
        Context.SaveChanges();
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _entities.RemoveRange(entities);
        Context.SaveChanges();
    }
    public void SaveChanges()
        => Context.SaveChanges();
}