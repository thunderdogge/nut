using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nut.Core.Messenger.Runners;
using Nut.Core.Messenger.Subscriptions;

namespace Nut.Core.Messenger
{
    public class NutMessenger : INutMessenger
    {
        private readonly Dictionary<Type, Dictionary<Guid, NutBaseSubscription>> subscriptions = new Dictionary<Type, Dictionary<Guid, NutBaseSubscription>>();

        public NutSubscriptionToken Subscribe<TMessage>(Action<TMessage> deliveryAction, NutSubscriptionReference reference = NutSubscriptionReference.Weak, string tag = null) where TMessage : NutMessage
        {
            return SubscribeInternal(deliveryAction, new NutSimpleMessengerRunner(), reference, tag);
        }

        public NutSubscriptionToken SubscribeOnMainThread<TMessage>(Action<TMessage> deliveryAction, NutSubscriptionReference reference = NutSubscriptionReference.Weak, string tag = null) where TMessage : NutMessage
        {
            return SubscribeInternal(deliveryAction, new NutMainThreadMessengerRunner(), reference, tag);
        }

        public NutSubscriptionToken SubscribeOnThreadPoolThread<TMessage>(Action<TMessage> deliveryAction, NutSubscriptionReference reference = NutSubscriptionReference.Weak, string tag = null) where TMessage : NutMessage
        {
            return SubscribeInternal(deliveryAction, new NutThreadPoolMessengerRunner(), reference, tag);
        }

        private NutSubscriptionToken SubscribeInternal<TMessage>(Action<TMessage> deliveryAction, INutMessengerRunner actionRunner, NutSubscriptionReference reference, string tag) where TMessage : NutMessage
        {
            if (deliveryAction == null)
            {
                throw new ArgumentNullException(nameof(deliveryAction));
            }

            NutBaseSubscription subscription;

            switch (reference)
            {
                case NutSubscriptionReference.Strong:
                    subscription = new NutStrongSubscription<TMessage>(actionRunner, deliveryAction, tag);
                    break;

                case NutSubscriptionReference.Weak:
                    subscription = new NutWeakSubscription<TMessage>(actionRunner, deliveryAction, tag);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(reference), "reference type unexpected " + reference);
            }

            lock (this)
            {
                Dictionary<Guid, NutBaseSubscription> messageSubscriptions;
                if (!subscriptions.TryGetValue(typeof(TMessage), out messageSubscriptions))
                {
                    messageSubscriptions = new Dictionary<Guid, NutBaseSubscription>();
                    subscriptions[typeof(TMessage)] = messageSubscriptions;
                }

                Nuts.Info($"Adding subscription {subscription.Id} for {typeof(TMessage).Name}");
                messageSubscriptions[subscription.Id] = subscription;

                PublishSubscriberChangeMessage<TMessage>(messageSubscriptions);
            }

            return new NutSubscriptionToken(
                            subscription.Id,
                            () => InternalUnsubscribe<TMessage>(subscription.Id),
                            deliveryAction);
        }

        public void Unsubscribe<TMessage>(NutSubscriptionToken nutSubscriptionId) where TMessage : NutMessage
        {
            InternalUnsubscribe<TMessage>(nutSubscriptionId.Id);
        }

        private void InternalUnsubscribe<TMessage>(Guid subscriptionGuid) where TMessage : NutMessage
        {
            lock (this)
            {
                Dictionary<Guid, NutBaseSubscription> messageSubscriptions;

                if (subscriptions.TryGetValue(typeof(TMessage), out messageSubscriptions))
                {
                    if (messageSubscriptions.ContainsKey(subscriptionGuid))
                    {
                        Nuts.Info($"Removing subscription {subscriptionGuid}");
                        messageSubscriptions.Remove(subscriptionGuid);
                    }
                }

                PublishSubscriberChangeMessage<TMessage>(messageSubscriptions);
            }
        }

        protected virtual void PublishSubscriberChangeMessage<TMessage>(Dictionary<Guid, NutBaseSubscription> messageSubscriptions) where TMessage : NutMessage
        {
            PublishSubscriberChangeMessage(typeof(TMessage), messageSubscriptions);
        }

        protected virtual void PublishSubscriberChangeMessage(Type messageType, Dictionary<Guid, NutBaseSubscription> messageSubscriptions)
        {
            var newCount = messageSubscriptions?.Count ?? 0;
            Publish(new NutSubscriberChangeMessage(this, messageType, newCount));
        }

        public bool HasSubscriptionsFor<TMessage>() where TMessage : NutMessage
        {
            lock (this)
            {
                Dictionary<Guid, NutBaseSubscription> messageSubscriptions;
                if (!subscriptions.TryGetValue(typeof(TMessage), out messageSubscriptions))
                {
                    return false;
                }
                return messageSubscriptions.Any();
            }
        }

        public int CountSubscriptionsFor<TMessage>() where TMessage : NutMessage
        {
            lock (this)
            {
                Dictionary<Guid, NutBaseSubscription> messageSubscriptions;
                if (!subscriptions.TryGetValue(typeof(TMessage), out messageSubscriptions))
                {
                    return 0;
                }
                return messageSubscriptions.Count;
            }
        }

        public bool HasSubscriptionsForTag<TMessage>(string tag) where TMessage : NutMessage
        {
            lock (this)
            {
                Dictionary<Guid, NutBaseSubscription> messageSubscriptions;
                if (!subscriptions.TryGetValue(typeof(TMessage), out messageSubscriptions))
                {
                    return false;
                }
                return messageSubscriptions.Any(x => x.Value.Tag == tag);
            }
        }

        public int CountSubscriptionsForTag<TMessage>(string tag) where TMessage : NutMessage
        {
            lock (this)
            {
                Dictionary<Guid, NutBaseSubscription> messageSubscriptions;
                if (!subscriptions.TryGetValue(typeof(TMessage), out messageSubscriptions))
                {
                    return 0;
                }
                return messageSubscriptions.Count(x => x.Value.Tag == tag);
            }
        }

        public IList<string> GetSubscriptionTagsFor<TMessage>() where TMessage : NutMessage
        {
            lock (this)
            {
                Dictionary<Guid, NutBaseSubscription> messageSubscriptions;
                if (!subscriptions.TryGetValue(typeof(TMessage), out messageSubscriptions))
                {
                    return new List<string>(0);
                }
                return messageSubscriptions.Select(x => x.Value.Tag).ToList();
            }
        }

        public void Publish<TMessage>(TMessage message) where TMessage : NutMessage
        {
            if (typeof(TMessage) == typeof(NutMessage))
            {
                Nuts.Warn("NutMessage publishing not allowed - this normally suggests non-specific generic used in calling code - switching to message.GetType()");
                Publish(message, message.GetType());
                return;
            }
            Publish(message, typeof(TMessage));
        }

        public void Publish(NutMessage message)
        {
            Publish(message, message.GetType());
        }

        public void Publish(NutMessage message, Type messageType)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            List<NutBaseSubscription> toNotify = null;
            lock (this)
            {
                Dictionary<Guid, NutBaseSubscription> messageSubscriptions;
                if (subscriptions.TryGetValue(messageType, out messageSubscriptions))
                {
                    Nuts.Info($"Found {messageSubscriptions.Values.Count} messages of type {messageType.Name}");
                    toNotify = messageSubscriptions.Values.ToList();
                }
            }

            if (toNotify == null || toNotify.Count == 0)
            {
                Nuts.Info($"Nothing registered for messages of type {messageType.Name}");
                return;
            }

            var allSucceeded = true;
            foreach (var subscription in toNotify)
            {
                allSucceeded &= subscription.Invoke(message);
            }

            if (!allSucceeded)
            {
                Nuts.Info("One or more listeners failed - purge scheduled");
                SchedulePurge(messageType);
            }
        }

        public void RequestPurge(Type messageType)
        {
            SchedulePurge(messageType);
        }

        public void RequestPurgeAll()
        {
            lock (this)
            {
                SchedulePurge(subscriptions.Keys.ToArray());
            }
        }

        private readonly Dictionary<Type, bool> _scheduledPurges = new Dictionary<Type, bool>();

        private void SchedulePurge(params Type[] messageTypes)
        {
            lock (this)
            {
                var threadPoolTaskAlreadyRequested = _scheduledPurges.Count > 0;
                foreach (var messageType in messageTypes)
                    _scheduledPurges[messageType] = true;

                if (!threadPoolTaskAlreadyRequested)
                {
                    Task.Run(() => DoPurge());
                }
            }
        }

        private void DoPurge()
        {
            List<Type> toPurge = null;
            lock (this)
            {
                toPurge = _scheduledPurges.Select(x => x.Key).ToList();
                _scheduledPurges.Clear();
            }

            foreach (var type in toPurge)
            {
                PurgeMessagesOfType(type);
            }
        }

        private void PurgeMessagesOfType(Type type)
        {
            lock (this)
            {
                Dictionary<Guid, NutBaseSubscription> messageSubscriptions;
                if (!subscriptions.TryGetValue(type, out messageSubscriptions))
                {
                    return;
                }

                var deadSubscriptionIds = new List<Guid>();
                foreach (var subscription in messageSubscriptions)
                {
                    if (!subscription.Value.IsAlive)
                    {
                        deadSubscriptionIds.Add(subscription.Key);
                    }
                }

                Nuts.Info($"Purging {deadSubscriptionIds.Count} subscriptions");
                foreach (var id in deadSubscriptionIds)
                {
                    messageSubscriptions.Remove(id);
                }

                PublishSubscriberChangeMessage(type, messageSubscriptions);
            }
        }
    }
}