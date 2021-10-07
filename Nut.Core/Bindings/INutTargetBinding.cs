using System;

namespace Nut.Core.Bindings
{
    public interface INutTargetBinding : IDisposable
    {
        NutBindingMode BindingMode { get; }
        void SetValue(object value);
        void SubscribeToEvents();
        void UnsubscribeFromEvents();
        event EventHandler<NutTargetChangedEventArgs> ValueChanged;
    }
}