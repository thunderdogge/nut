using System;

namespace Nut.Core.Messenger
{
    public abstract class NutMessage
    {
        public object Sender { get; private set; }

        protected NutMessage(object sender)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            Sender = sender;
        }
    }
}