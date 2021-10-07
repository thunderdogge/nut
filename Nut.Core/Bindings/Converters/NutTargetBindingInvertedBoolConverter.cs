using Nut.Ioc;
using static Nut.Core.Extensions.NutObjectExtensions;

namespace Nut.Core.Bindings.Converters
{
    [NutIocIgnore]
    public class NutTargetBindingInvertedBoolConverter : INutTargetBindingConverter
    {
        public object Convert(object value)
        {
            return !IsTrueValue(value);
        }

        public object ConvertBack(object value)
        {
            return !IsTrueValue(value);
        }
    }
}