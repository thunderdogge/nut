using System;
using System.Collections.Generic;

namespace Nut.Core.Messenger
{
    public interface INutMessenger
    {
        NutSubscriptionToken Subscribe<TMessage>(Action<TMessage> deliveryAction, NutSubscriptionReference reference = NutSubscriptionReference.Weak, string tag = null) where TMessage : NutMessage;

        NutSubscriptionToken SubscribeOnMainThread<TMessage>(Action<TMessage> deliveryAction, NutSubscriptionReference reference = NutSubscriptionReference.Weak, string tag = null) where TMessage : NutMessage;

        NutSubscriptionToken SubscribeOnThreadPoolThread<TMessage>(Action<TMessage> deliveryAction, NutSubscriptionReference reference = NutSubscriptionReference.Weak, string tag = null) where TMessage : NutMessage;

        void Unsubscribe<TMessage>(NutSubscriptionToken nutSubscriptionId) where TMessage : NutMessage;

        bool HasSubscriptionsFor<TMessage>() where TMessage : NutMessage;

        int CountSubscriptionsFor<TMessage>() where TMessage : NutMessage;

        bool HasSubscriptionsForTag<TMessage>(string tag) where TMessage : NutMessage;

        int CountSubscriptionsForTag<TMessage>(string tag) where TMessage : NutMessage;

        IList<string> GetSubscriptionTagsFor<TMessage>() where TMessage : NutMessage;

        void Publish<TMessage>(TMessage message) where TMessage : NutMessage;

        void Publish(NutMessage message);

        void Publish(NutMessage message, Type messageType);

        void RequestPurge(Type messageType);

        void RequestPurgeAll();
    }
}