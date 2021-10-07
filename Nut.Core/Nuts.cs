using System;
using System.Collections.Generic;
using Nut.Core.Dependencies;
using Nut.Core.Logging;
using Nut.Core.Platform;

namespace Nut.Core
{
    public static class Nuts
    {
        private const string DefaultLogTag = "nutty";

        public static void Info(string message, string tag = DefaultLogTag)
        {
            NutSingleton<INutLogger>.Instance.Info(tag, message);
        }

        public static void Warn(string message, string tag = DefaultLogTag)
        {
            NutSingleton<INutLogger>.Instance.Warn(tag, message);
        }

        public static void Error(string message, string tag = DefaultLogTag)
        {
            NutSingleton<INutLogger>.Instance.Error(tag, message);
        }

        public static void Error(string message, Exception exception, string tag = DefaultLogTag)
        {
            NutSingleton<INutLogger>.Instance.Error(tag, message, exception);
        }

        public static void Debug(string message, string tag = DefaultLogTag)
        {
            NutSingleton<INutLogger>.Instance.Debug(tag, message);
        }

        public static IDisposable Trace(string message, string tag = DefaultLogTag, NutLoggerLevel level = NutLoggerLevel.Info)
        {
            return NutSingleton<INutLogger>.Instance.Trace(tag, message, level);
        }

        public static object Create(Type type)
        {
            return NutSingleton<INutIocProvider>.Instance.Create(type);
        }

        public static TService Create<TService>()
        {
            return NutSingleton<INutIocProvider>.Instance.Create<TService>();
        }

        public static object Resolve(Type type)
        {
            return NutSingleton<INutIocProvider>.Instance.Resolve(type);
        }

        public static TService Resolve<TService>()
        {
            return NutSingleton<INutIocProvider>.Instance.Resolve<TService>();
        }

        public static bool TryResolve<TService>(out TService service)
        {
            var instance = NutSingleton<INutIocProvider>.Instance;
            if (instance == null)
            {
                service = default(TService);
                return false;
            }

            return instance.TryResolve(out service);
        }

        public static TService SafeResolve<TService>()
        {
            var instance = NutSingleton<INutIocProvider>.Instance;
            return instance == null ? default(TService) : instance.SafeResolve<TService>();
        }

        public static void RegisterUnique<TInterface, TService>() where TService : TInterface
        {
            NutSingleton<INutIocProvider>.Instance.RegisterUnique<TInterface, TService>();
        }

        public static void RegisterFactory<TService>(Func<object> factory)
        {
            NutSingleton<INutIocProvider>.Instance.RegisterFactory<TService>(factory);
        }

        public static void RegisterSingleton<TService>(TService singleton)
        {
            NutSingleton<INutIocProvider>.Instance.RegisterSingleton(singleton);
        }

        public static void RegisterServiceDescription(Type serviceType, Func<Func<Type, object>, object> resolveParameters)
        {
            NutSingleton<INutIocProvider>.Instance.RegisterServiceDescription(serviceType, resolveParameters);
        }

        public static void RegisterServiceDescriptions(IEnumerable<KeyValuePair<Type, Func<Func<Type, object>, object>>> descriptions)
        {
            NutSingleton<INutIocProvider>.Instance.RegisterServiceDescriptions(descriptions);
        }
    }
}