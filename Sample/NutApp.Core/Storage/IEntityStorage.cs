using System;
using System.Linq.Expressions;
using NutApp.Core.Business;

namespace NutApp.Core.Storage
{
    public interface IEntityStorage
    {
        TEntity Get<TEntity>(Guid id) where TEntity : class, IEntity;
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;
        TEntity[] GetAll<TEntity>() where TEntity : class, IEntity;
        void Write<TEntity>(TEntity item) where TEntity : class, IEntity;
        void Write<TEntity>(TEntity[] items) where TEntity : class, IEntity;
        void Remove<TEntity>(Guid id) where TEntity : class, IEntity;
        void Remove<TEntity>(Guid[] ids) where TEntity : class, IEntity;
        void Erase<TEntity>() where TEntity : class, IEntity;
    }
}