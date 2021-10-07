using System;
using Nut.Core.Bindings.Converters;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public class NutUnifiedTargetBindingConverter : INutTargetBindingConverter
    {
        private readonly Func<object, object> converterMethod;

        public NutUnifiedTargetBindingConverter(Func<object, object> converterMethod)
        {
            this.converterMethod = converterMethod;
        }

        public object Convert(object value)
        {
            return converterMethod.Invoke(value);
        }

        public object ConvertBack(object value)
        {
            return value;
        }
    }
}