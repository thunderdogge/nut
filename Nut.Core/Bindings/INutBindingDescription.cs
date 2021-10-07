using System;

namespace Nut.Core.Bindings
{
    public interface INutBindingDescription
    {
        NutBindingMode Mode { get; set; }
        string ConverterName { get; set; }
        Func<object, object> ConverterMethod { get; set; }
        string TargetProperty { get; set; }
        string SourceProperty { get; set; }
    }
}