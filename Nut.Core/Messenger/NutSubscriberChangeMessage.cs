using System;

namespace Nut.Core.Messenger
{
    public class NutSubscriberChangeMessage : NutMessage
    {
        public Type MessageType { get; private set; }
        public int SubscriberCount { get; private set; }

        public NutSubscriberChangeMessage(object sender, Type messageType, int countSubscribers = 0) : base(sender)
        {
            SubscriberCount = countSubscribers;
            MessageType = messageType;
        }
    }
}