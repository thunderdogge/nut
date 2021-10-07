using System;
using System.Linq;
using System.Linq.Expressions;
using NutApp.Core.Business;
using SQLite.Net;

namespace NutApp.Core.Storage
{
    public class EntityStorage : IEntityStorage
    {
        private readonly EntityStorageConnection connection;

        public EntityStorage(IEntityStorageSettings settings,
                             IEntityStorageSerializer serializer)
        {
            var connectionString = new SQLiteConnectionString(settings.Path, true, serializer);
            connection = new EntityStorageConnection(settings.Platform, connectionString);
        }

        public TEntity Get<TEntity>(Guid id) where TEntity : class, IEntity
        {
            using (connection.Lock())
            {
                return connection.Find<TEntity>(id.ToString());
            }
        }

        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity
        {
            using (connection.Lock())
            {
                return connection.Find(predicate);
            }
        }

        public TEntity[] GetAll<TEntity>() where TEntity : class, IEntity
        {
            using (connection.Lock())
            {
                return connection.Table<TEntity>().ToArray();
            }
        }

        public void Write<TEntity>(TEntity item) where TEntity : class, IEntity
        {
            using (connection.Lock())
            {
                connection.InsertOrReplace(item);
            }
        }

        public void Write<TEntity>(TEntity[] items) where TEntity : class, IEntity
        {
            using (connection.Lock())
            {
                connection.InsertOrReplaceAll(items);
            }
        }

        public void Remove<TEntity>(Guid id) where TEntity : class, IEntity
        {
            using (connection.Lock())
            {
                connection.Delete<TEntity>(id.ToString());
            }
        }

        public void Remove<TEntity>(Guid[] ids) where TEntity : class, IEntity
        {
            using (connection.Lock())
            {
                connection.RunInTransaction(() =>
                {
                    foreach (var id in ids)
                    {
                        Remove<TEntity>(id);
                    }
                });
            }
        }

        public void Erase<TEntity>() where TEntity : class, IEntity
        {
            using (connection.Lock())
            {
                connection.DeleteAll<TEntity>();
            }
        }
    }
}