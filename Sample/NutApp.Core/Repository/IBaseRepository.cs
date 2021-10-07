using System;
using NutApp.Core.Business;

namespace NutApp.Core.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : IEntity
    {
        TEntity Get(Guid id);
        TEntity[] GetAll();
        void Write(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Guid id);
    }
}