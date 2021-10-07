using Nut.Core.Models;

namespace Nut.Core.Navigation
{
    public interface INutViewPresenter
    {
        void Show(NutViewModelRequest viewModelRequest);
        void Close(INutViewModel viewModel);
    }
}