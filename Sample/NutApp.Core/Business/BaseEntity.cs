using System;
using SQLite.Net.Attributes;

namespace NutApp.Core.Business
{
    public abstract class BaseEntity : IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
    }
}