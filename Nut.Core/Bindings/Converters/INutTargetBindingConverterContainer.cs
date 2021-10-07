namespace Nut.Core.Bindings.Converters
{
    public interface INutTargetBindingConverterContainer
    {
        INutTargetBindingConverter GetBindingConverter(string name);
        void RegisterBindingConverter(string name, INutTargetBindingConverter bindingConverter);
    }
}