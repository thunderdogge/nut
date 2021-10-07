using Nut.Core.Bindings;

namespace Nut.iOS.Controls
{
    public interface INutTableCell : INutBindingStore
    {
        object DataSource { get; set; }
    }
}