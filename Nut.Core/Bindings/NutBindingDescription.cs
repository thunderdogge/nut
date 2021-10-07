using System;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public class NutBindingDescription : INutBindingDescription
    {
        public NutBindingMode Mode { get; set; }
        public string ConverterName { get; set; }
        public Func<object, object> ConverterMethod { get; set; }
        public string TargetProperty { get; set; }
        public string SourceProperty { get; set; }

        public override string ToString()
        {
            return $"Bind `{TargetProperty}` to `{SourceProperty}` ({Mode} {ConverterName})";
        }
    }
}