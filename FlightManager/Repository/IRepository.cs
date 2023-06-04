using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Factory de repository générique.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>                                                                                                                                                                                                                                               
        IRepository<T> GetRepository<T>() where T : class;
        IQueryable<TEntity> Fetch(Expression<Func<TEntity, bool>> filter = null);

        int Count(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        void Add(TEntity entity, bool andSaveChanges = true);
        void AddCollection(List<TEntity> entities, bool andSaveChanges = true);
        void Delete(TEntity entity, bool andSaveChanges = true);
        void DeleteCollection(Expression<Func<TEntity, bool>> predicate, bool andSaveChanges = true);
        void SaveChanges();
    }
}
