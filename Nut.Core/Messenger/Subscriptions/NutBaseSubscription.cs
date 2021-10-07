using System;
using Nut.Core.Messenger.Runners;

namespace Nut.Core.Messenger.Subscriptions
{
    public abstract class NutBaseSubscription
    {
        private readonly INutMessengerRunner actionRunner;

        public Guid Id { get; }
        public string Tag { get; }
        public abstract bool IsAlive { get; }

        public abstract bool Invoke(object message);

        protected NutBaseSubscription(INutMessengerRunner actionRunner, string tag)
        {
            this.actionRunner = actionRunner;
            Id = Guid.NewGuid();
            Tag = tag;
        }

        protected void Call(Action action)
        {
            actionRunner.Run(action);
        }
    }
}