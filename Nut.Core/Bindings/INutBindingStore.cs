namespace Nut.Core.Bindings
{
    public interface INutBindingStore
    {
        INutBindingContext BindingContext { get; set; }
    }
}