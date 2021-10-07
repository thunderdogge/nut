namespace Nut.Core.Bindings
{
    public interface INutTargetBindingFactory
    {
        INutTargetBinding CreateBinding(object target);
    }
}