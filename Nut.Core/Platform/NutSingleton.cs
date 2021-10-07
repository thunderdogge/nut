using System;
using Nut.Ioc;

namespace Nut.Core.Platform
{
    [NutIocIgnore]
    public abstract class NutSingleton<TInterface> : IDisposable where TInterface : class
    {
        public static TInterface Instance { get; private set; }

        protected NutSingleton()
        {
            if (Instance != null)
            {
                throw new Exception($"Instance of NutSingleton<{typeof(TInterface).Name}> already created");
            }

            Instance = this as TInterface;
        }

        public virtual void Dispose()
        {
            Instance = default(TInterface);
        }
    }
}