using System;
using Nut.Core.Messenger.Runners;

namespace Nut.Core.Messenger.Subscriptions
{
    public class NutWeakSubscription<TMessage> : NutTypedSubscription<TMessage> where TMessage : NutMessage
    {
        private readonly WeakReference weakReference;

        public override bool IsAlive => weakReference.IsAlive;

        protected override bool TypedInvoke(TMessage message)
        {
            if (!weakReference.IsAlive)
            {
                return false;
            }

            var action = weakReference.Target as Action<TMessage>;
            if (action == null)
            {
                return false;
            }

            Call(() =>
            {
                action?.Invoke(message);
            });

            return true;
        }

        public NutWeakSubscription(INutMessengerRunner actionRunner, Action<TMessage> listener, string tag) : base(actionRunner, tag)
        {
            weakReference = new WeakReference(listener);
        }
    }
}