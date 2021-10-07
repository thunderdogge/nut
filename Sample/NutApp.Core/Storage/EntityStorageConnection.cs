using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nut.Ioc;
using SQLite.Net;
using SQLite.Net.Interop;

namespace NutApp.Core.Storage
{
    [NutIocIgnore]
    public class EntityStorageConnection : SQLiteConnectionWithLock
    {
        private HashSet<Type> createdTables = new HashSet<Type>();

        public EntityStorageConnection(ISQLitePlatform sqlitePlatform,
                                       SQLiteConnectionString connectionString) : base(sqlitePlatform, connectionString)
        {
        }

        public new TEntity Find<TEntity>(object pk) where TEntity : class
        {
            EnsureTableCreated<TEntity>();
            return base.Find<TEntity>(pk);
        }

        public new TEntity Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            EnsureTableCreated<TEntity>();
            return base.Find(predicate);
        }

        public new TableQuery<TEntity> Table<TEntity>() where TEntity : class
        {
            EnsureTableCreated<TEntity>();
            return base.Table<TEntity>();
        }

        public new List<TEntity> Query<TEntity>(string query, params object[] args) where TEntity : class
        {
            EnsureTableCreated<TEntity>();
            return base.Query<TEntity>(query, args);
        }

        private void EnsureTableCreated<TEntity>()
        {
            var type = typeof(TEntity);
            if (createdTables.Add(type))
            {
                CreateTable(type);
            }
        }
    }

}