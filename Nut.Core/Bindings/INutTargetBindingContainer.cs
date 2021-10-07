namespace Nut.Core.Bindings
{
    public interface INutTargetBindingContainer
    {
        INutTargetBinding GetTargetBinding(object target, string property);
        void RegisterTargetBindingFactory(string name, INutTargetBindingFactory factory);
    }
}