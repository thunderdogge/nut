using System;
using Nut.Core.Messenger.Runners;
using Nut.Ioc;

namespace Nut.Core.Messenger.Subscriptions
{
    [NutIocIgnore]
    public abstract class NutTypedSubscription<TMessage> : NutBaseSubscription where TMessage : NutMessage
    {
        protected NutTypedSubscription(INutMessengerRunner actionRunner, string tag) : base(actionRunner, tag)
        {
        }

        public sealed override bool Invoke(object message)
        {
            var typedMessage = message as TMessage;
            if (typedMessage == null)
            {
                throw new Exception($"Unexpected message {message}");
            }

            return TypedInvoke(typedMessage);
        }

        protected abstract bool TypedInvoke(TMessage message);
    }
}