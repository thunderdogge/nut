using System;
using Nut.Core.Models;
using Nut.Core.Navigation;
using Nut.Core.Platform;
using Nut.Core.Views;
using UIKit;

namespace Nut.iOS.Views
{
    public class NutIosViewPresenter : NutViewPresenter
    {
        private readonly INutViewMapper viewMapper;
        private readonly INutIosViewMonitor viewMonitor;

        public NutIosViewPresenter(INutViewMapper viewMapper,
                                   INutIosViewMonitor viewMonitor,
                                   INutMainThreadDispatcher dispatcher) : base(dispatcher)
        {
            this.viewMapper = viewMapper;
            this.viewMonitor = viewMonitor;
        }

        protected override void ShowViewModel(NutViewModelRequest viewModelRequest)
        {
            var navigationController = GetNavigationController(viewModelRequest.ViewModelType);
            if (navigationController == null)
            {
                ShowViewModelFirst(viewModelRequest);
                return;
            }

            if (viewModelRequest.Mode.HasFlag(NutViewModelRequestMode.ClearStack))
            {
                ShowViewModelClearStack(navigationController, viewModelRequest);
                return;
            }

            ShowViewModelSimple(navigationController, viewModelRequest);
        }

        protected virtual void ShowViewModelFirst(NutViewModelRequest viewModelRequest)
        {
            var rootController = CreateViewController(viewModelRequest);
            var navigationController = new UINavigationController(rootController);
            viewMonitor.Window.RootViewController = navigationController;
        }

        protected virtual void ShowViewModelSimple(UINavigationController navigationController, NutViewModelRequest viewModelRequest)
        {
            var viewController = CreateViewController(viewModelRequest);
            navigationController.PushViewController(viewController, true);
        }

        protected virtual void ShowViewModelClearStack(UINavigationController navigationController, NutViewModelRequest viewModelRequest)
        {
            var viewController = CreateViewController(viewModelRequest);
            navigationController.SetViewControllers(new[] { viewController }, true);
        }

        protected override void CloseViewModel(INutViewModel viewModel)
        {
            var navController = GetNavigationController(viewModel.GetType());
            if (navController.TopViewController == null)
            {
                throw new ArgumentNullException(nameof(navController.TopViewController), "Attempt to close ViewModel with null controller");
            }

            navController.PopViewController(true);
        }

        protected virtual UINavigationController GetNavigationController(Type viewModelType)
        {
            return viewMonitor.Window.RootViewController as UINavigationController;
        }

        protected virtual UIViewController CreateViewController(NutViewModelRequest viewModelRequest)
        {
            var controllerType = viewMapper.GetViewType(viewModelRequest.ViewModelType);
            var controllerInstance = Activator.CreateInstance(controllerType) as UIViewController;

            var nutControllerInstance = controllerInstance as INutIosView;
            if (nutControllerInstance != null)
            {
                nutControllerInstance.ViewModelParameters = viewModelRequest.ViewModelParameters;
                return controllerInstance;
            }

            throw new ArgumentException("Controller must be convertible to `{0}`", nameof(INutIosView));
        }
    }
}