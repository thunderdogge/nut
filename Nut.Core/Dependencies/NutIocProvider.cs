using System;
using System.Collections.Generic;
using System.Reflection;
using Nut.Core.Platform;
using Nut.Ioc;

namespace Nut.Core.Dependencies
{
    [NutIocIgnore]
    public class NutIocProvider : NutSingleton<INutIocProvider>, INutIocProvider
    {
        private HashSet<Type> uniqueTypes;
        private Dictionary<Type, object> singletonCache;
        private Dictionary<Type, Func<object>> serviceFactories;
        private Dictionary<Type, Func<Func<Type, object>, object>> serviceDescriptions;

        public NutIocProvider()
        {
            singletonCache = new Dictionary<Type, object>();
            serviceDescriptions = new Dictionary<Type, Func<Func<Type, object>, object>>();
        }

        public TService Create<TService>()
        {
            return (TService)Create(typeof(TService));
        }

        public virtual object Create(Type type)
        {
            return ConstructInternal(type);
        }

        public TService Resolve<TService>()
        {
            return (TService) Resolve(typeof(TService));
        }

        public virtual object Resolve(Type type)
        {
            return ResolveInternal(type);
        }

        public object SafeResolve(Type type)
        {
            object service;
            return TryResolve(type, out service) ? service : null;
        }

        public TService SafeResolve<TService>()
        {
            TService service;
            return TryResolve(out service) ? service : default(TService);
        }

        public bool TryResolve<TService>(out TService service)
        {
            object resolved;
            if (TryResolve(typeof(TService), out resolved))
            {
                service = (TService) resolved;
                return true;
            }

            service = default(TService);
            return false;
        }

        public bool TryResolve(Type type, out object service)
        {
            try
            {
                service = Resolve(type);
                return true;
            }
            catch
            {
                service = null;
                return false;
            }
        }

        public void RegisterUnique<TInterface, TService>() where TService : TInterface
        {
            if (uniqueTypes == null)
            {
                uniqueTypes = new HashSet<Type>();
            }

            uniqueTypes.Add(typeof(TService));
            uniqueTypes.Add(typeof(TInterface));
        }

        public void RegisterFactory<TService>(Func<object> factory)
        {
            if (serviceFactories == null)
            {
                serviceFactories = new Dictionary<Type, Func<object>>();
            }

            serviceFactories[typeof(TService)] = factory;
        }

        public void RegisterSingleton<TService>(TService singleton)
        {
            singletonCache[typeof(TService)] = singleton;
        }

        public void RegisterServiceDescription(Type serviceType, Func<Func<Type, object>, object> resolveParameters)
        {
            serviceDescriptions[serviceType] = resolveParameters;
        }

        public void RegisterServiceDescriptions(IEnumerable<KeyValuePair<Type, Func<Func<Type, object>, object>>> descriptions)
        {
            foreach (var description in descriptions)
            {
                RegisterServiceDescription(description.Key, description.Value);
            }
        }

        protected virtual object ResolveInternal(Type type)
        {
            if (uniqueTypes != null && uniqueTypes.Contains(type))
            {
                return ConstructInternal(type);
            }
            if (singletonCache.ContainsKey(type))
            {
                return singletonCache[type];
            }

            return singletonCache[type] = ConstructInternal(type);
        }

        protected virtual object ConstructInternal(Type serviceType)
        {
            if (serviceFactories != null && serviceFactories.ContainsKey(serviceType))
            {
                return serviceFactories[serviceType].Invoke();
            }

            if (serviceDescriptions.ContainsKey(serviceType))
            {
                return serviceDescriptions[serviceType].Invoke(Resolve);
            }

            return ConstructImplicitly(serviceType);
        }

        protected virtual object ConstructImplicitly(Type serviceType)
        {
            try
            {
                return Activator.CreateInstance(serviceType);
            }
            catch (Exception e)
            {
                var exception = e is TargetInvocationException ? e.InnerException : e;
                throw new InvalidOperationException($"Attempt to create `{serviceType}` failed", exception);
            }
        }
    }
}