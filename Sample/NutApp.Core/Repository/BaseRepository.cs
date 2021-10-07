using System;
using NutApp.Core.Business;
using NutApp.Core.Storage;

namespace NutApp.Core.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IEntityStorage entityStorage;

        protected BaseRepository(IEntityStorage entityStorage)
        {
            this.entityStorage = entityStorage;
        }

        public virtual TEntity Get(Guid id)
        {
            return entityStorage.Get<TEntity>(id);
        }

        public virtual TEntity[] GetAll()
        {
            return entityStorage.GetAll<TEntity>();
        }

        public virtual void Write(TEntity entity)
        {
            entityStorage.Write(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public virtual void Delete(Guid id)
        {
            entityStorage.Remove<TEntity>(id);
        }
    }
}