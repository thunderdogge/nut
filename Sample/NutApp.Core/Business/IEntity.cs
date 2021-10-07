using System;

namespace NutApp.Core.Business
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}