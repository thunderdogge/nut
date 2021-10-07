using System;
using Nut.Core.Models;
using Nut.Core.Platform;
using Nut.Ioc;

namespace Nut.Core.Navigation
{
    [NutIocIgnore]
    public abstract class NutNavigatableObject : NutNotifyPropertyChanged
    {
        private INutViewPresenter presenter;
        private INutViewPresenter Presenter => presenter ?? (presenter = Nuts.Resolve<INutViewPresenter>());

        private INutMainThreadDispatcher dispatcher;
        private INutMainThreadDispatcher Dispatcher => dispatcher ?? (dispatcher = Nuts.Resolve<INutMainThreadDispatcher>());

        protected void ShrinkViewModel<TViewModel>(object initParams = null) where TViewModel : INutViewModel
        {
            ShowViewModel(typeof(TViewModel), initParams, NutViewModelRequestMode.NewTask | NutViewModelRequestMode.ClearStack);
        }

        protected void ShowViewModel<TViewModel>(object initParams = null, NutViewModelRequestMode requestMode = NutViewModelRequestMode.Default) where TViewModel : INutViewModel
        {
            ShowViewModel(typeof(TViewModel), initParams, requestMode);
        }

        protected void ShowViewModel(Type viewModelType, object initParams = null, NutViewModelRequestMode requestMode = NutViewModelRequestMode.Default)
        {
            var request = new NutViewModelRequest
            {
                Mode = requestMode,
                ViewModelType = viewModelType,
                ViewModelParameters = initParams
            };

            Presenter.Show(request);
        }

        protected void CloseViewModel(INutViewModel viewModel)
        {
            Presenter.Close(viewModel);
        }

        protected void RunMainThread(Action action)
        {
            Dispatcher.RunMainThreadAction(action);
        }
    }
}