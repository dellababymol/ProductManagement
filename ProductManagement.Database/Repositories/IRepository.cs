using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Database.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity Get(Guid id);

    IEnumerable<TEntity> GetAll();

    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);

    void Update(TEntity entity);

    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);

    void SaveChanges();
}
