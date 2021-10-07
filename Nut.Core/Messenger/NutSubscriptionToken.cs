using System;

namespace Nut.Core.Messenger
{
    public sealed class NutSubscriptionToken : IDisposable
    {
        private readonly Action disposeMe;
        private readonly object[] dependentObjects;

        public NutSubscriptionToken(Guid id, Action disposeMe, params object[] dependentObjects)
        {
            Id = id;

            this.disposeMe = disposeMe;
            this.dependentObjects = dependentObjects;
        }

        public Guid Id { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                disposeMe();
            }
        }
    }
}