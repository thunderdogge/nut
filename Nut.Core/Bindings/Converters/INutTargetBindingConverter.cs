namespace Nut.Core.Bindings.Converters
{
    public interface INutTargetBindingConverter
    {
        object Convert(object value);
        object ConvertBack(object value);
    }
}