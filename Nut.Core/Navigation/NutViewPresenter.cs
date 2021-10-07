using System;
using Nut.Core.Models;
using Nut.Core.Platform;

namespace Nut.Core.Navigation
{
    public abstract class NutViewPresenter : INutViewPresenter
    {
        private readonly INutMainThreadDispatcher dispatcher;

        protected NutViewPresenter(INutMainThreadDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public void Show(NutViewModelRequest viewModelRequest)
        {
            if (viewModelRequest == null)
            {
                throw new ArgumentNullException(nameof(viewModelRequest), "ViewModel request is missing");
            }

            dispatcher.RunMainThreadAction(() => ShowViewModel(viewModelRequest));
        }

        public void Close(INutViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "Attempt to close ViewModel that is null");
            }

            dispatcher.RunMainThreadAction(() => CloseViewModel(viewModel));
        }

        protected abstract void ShowViewModel(NutViewModelRequest viewModelRequest);
        protected abstract void CloseViewModel(INutViewModel viewModel);
    }
}