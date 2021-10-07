using System;

namespace Nut.Core.Platform
{
    public class NutDisposableCallback : IDisposable
    {
        private volatile bool isDisposed;
        private readonly Action callback;

        public NutDisposableCallback(Action callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            this.callback = callback;
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                callback();
            }
        }
    }
}