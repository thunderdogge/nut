using Nut.Core.Views;

namespace Nut.iOS.Views
{
    public interface INutIosView : INutView
    {
        object ViewModelParameters { get; set; }
    }
}