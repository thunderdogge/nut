using System;
using System.Collections.Generic;

namespace Nut.Core.Dependencies
{
    public interface INutIocProvider : IDisposable
    {
        TService Create<TService>();
        object Create(Type type);
        TService Resolve<TService>();
        object Resolve(Type type);
        object SafeResolve(Type type);
        TService SafeResolve<TService>();
        bool TryResolve<TService>(out TService service);
        bool TryResolve(Type type, out object service);
        void RegisterUnique<TInterface, TService>() where TService : TInterface;
        void RegisterFactory<TService>(Func<object> factory);
        void RegisterSingleton<TService>(TService singleton);
        void RegisterServiceDescription(Type serviceType, Func<Func<Type, object>, object> resolveParameters);
        void RegisterServiceDescriptions(IEnumerable<KeyValuePair<Type, Func<Func<Type, object>, object>>> descriptions);
    }
}