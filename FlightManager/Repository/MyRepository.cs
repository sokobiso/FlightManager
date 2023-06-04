using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FlightManager.Repository
{
    public class MyRepository<TE> : IRepository<TE>, IDisposable
     where TE : class
    {
        private List<TE> _entities;

        public MyRepository()
        {
            _entities = DataSet();
        }

        public void Dispose()
        {
            if (_entities != null)
            {
                _entities = null;
            }
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new MyRepository<T>();
        }

        public IQueryable<TE> Fetch(Expression<Func<TE, bool>> filter = null)
        {
            IQueryable<TE> query = _entities.AsQueryable<TE>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public bool Any(Expression<Func<TE, bool>> predicate)
        {
            return _entities.AsQueryable().Any(predicate);
        }

        public int Count(Expression<Func<TE, bool>> predicate)
        {
            return _entities.AsQueryable().Count(predicate);
        }

        public TE Single(Expression<Func<TE, bool>> predicate)
        {
            return _entities.AsQueryable().Single(predicate);
        }

        public TE First(Expression<Func<TE, bool>> predicate)
        {
            return _entities.AsQueryable().First(predicate);
        }

        public TE FirstOrDefault(Expression<Func<TE, bool>> predicate)
        {
            return _entities.AsQueryable().FirstOrDefault(predicate);
        }

        public TE SingleOrDefault(Expression<Func<TE, bool>> predicate)
        {
            return _entities.AsQueryable().SingleOrDefault(predicate);
        }

        public List<TE> Get(Expression<Func<TE, bool>> filter = null, Func<IQueryable<TE>, IOrderedQueryable<TE>> orderBy = null)
        {
            return Get(filter, orderBy, null);
        }

        /// <summary>
        /// Paramètre includeProperties à purger, dépend de l'ancienne logique d'includes, piégeante à chaque renommage. Employer du Fetch(...).Include(...).ToList() à la place.
        /// </summary>
        public List<TE> Get(
            Expression<Func<TE, bool>> filter,
            Func<IQueryable<TE>, IOrderedQueryable<TE>> orderBy,
            string includeProperties)
        {
            IQueryable<TE> query = _entities.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //query = AddIncludePropertiesToQuery(includeProperties, query);

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query.ToList();
        }

        public void Delete(TE entity, bool andSaveChanges = true)
        {
            _entities.Remove(entity);
            SaveChanges(andSaveChanges);
        }

        public void DeleteCollection(Expression<Func<TE, bool>> predicate, bool andSaveChanges = true)
        {
            foreach (TE item in _entities.AsQueryable().Where(predicate))
            {
                _entities.Remove(item);
            }
            SaveChanges(andSaveChanges);
        }

        public virtual void Add(TE entity, bool andSaveChanges = true)
        {
            var count = _entities.Count();
            SetId(entity);
            _entities.Add(entity);
            SaveChanges(andSaveChanges);
        }

        public void AddCollection(List<TE> entities, bool andSaveChanges = true)
        {
            foreach (var item in entities)
            {
                SetId(item);
            }
            _entities.AddRange(entities);
            SaveChanges(andSaveChanges);
        }


        public void SaveChanges()
        {
            SaveChanges(true);
        }

        private void SaveChanges(bool reallyDoIt)
        {
        }

        private List<TE> DataSet()
        {
            if (_entities == null)
            {
                return new List<TE>();
            }
            return _entities;
        }
        private void SetId(TE item)
        {
            var count = _entities.Count();
            var id = count + 1;
            item.SetPropertyValue("Id", id);
        }
    }

    public static class PropertyExtension
    {
        public static void SetPropertyValue(this object obj, string propName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(propName);
            property.SetValue(obj, Convert.ChangeType(value, property.PropertyType), null);
        }

        public static Stream ToStream(this string str)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(str));
        }
        public static byte[] ReadFully(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
