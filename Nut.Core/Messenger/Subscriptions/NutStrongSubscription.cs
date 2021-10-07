using System;
using Nut.Core.Messenger.Runners;

namespace Nut.Core.Messenger.Subscriptions
{
    public class NutStrongSubscription<TMessage> : NutTypedSubscription<TMessage> where TMessage : NutMessage
    {
        private readonly Action<TMessage> _action;

        public override bool IsAlive => true;

        protected override bool TypedInvoke(TMessage message)
        {
            Call(() => _action?.Invoke(message));
            return true;
        }

        public NutStrongSubscription(INutMessengerRunner actionRunner, Action<TMessage> action, string tag) : base(actionRunner, tag)
        {
            _action = action;
        }
    }
}