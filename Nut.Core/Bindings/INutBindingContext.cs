namespace Nut.Core.Bindings
{
    public interface INutBindingContext
    {
        object DataSource { get; set; }
        void ApplyBindings();
        void RegisterBinding(INutBinding binding);
        void UnregisterBindings();
    }
}