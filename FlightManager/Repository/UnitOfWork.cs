using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void SaveChanges();
    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private static Dictionary<string, object> _repositories;

        public void Dispose()
        {
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
                return (IRepository<TEntity>)_repositories[type];

            var repositoryType = typeof(MyRepository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity))));

            return (IRepository<TEntity>)_repositories[type];
        }

        public UnitOfWork()
        {
        }

        public void SaveChanges()
        {
            //_context.SaveChanges();
        }

    }
}
