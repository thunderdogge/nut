using System;

namespace Nut.Core.Bindings
{
    public interface INutSourceBinding : IDisposable
    {
        object DataSource { get; set; }
        object BindingSource { get; }
        string PropertyName { get; }
        string PropertyFullName { get; }
        NutBindingMode Mode { get; }
        object GetValue();
        void SetValue(object newValue);
    }
}