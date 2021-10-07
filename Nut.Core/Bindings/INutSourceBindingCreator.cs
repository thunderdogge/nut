namespace Nut.Core.Bindings
{
    public interface INutSourceBindingCreator
    {
        INutSourceBinding Create(INutBindingDescription bindingDescription);
    }
}