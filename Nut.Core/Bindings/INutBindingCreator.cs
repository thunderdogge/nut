namespace Nut.Core.Bindings
{
    public interface INutBindingCreator
    {
        NutBinding Create(object bindingTarget, INutBindingDescription bindingDescription);
    }
}