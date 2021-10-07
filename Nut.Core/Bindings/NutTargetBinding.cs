using System;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public abstract class NutTargetBinding : INutTargetBinding
    {
        public virtual NutBindingMode BindingMode => NutBindingMode.OneWay;

        protected virtual void FireValueChanged(object newValue)
        {
            ValueChanged?.Invoke(this, new NutTargetChangedEventArgs(newValue));
        }

        public virtual void SubscribeToEvents()
        {
        }

        public virtual void UnsubscribeFromEvents()
        {
        }

        public abstract void SetValue(object value);

        public event EventHandler<NutTargetChangedEventArgs> ValueChanged;

        ~NutTargetBinding()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}