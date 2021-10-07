using System;
using Nut.Core.Models;
using Nut.Core.Serialization;
using Nut.Core.Views;
using UIKit;

namespace Nut.iOS.Views
{
    public class NutIosViewLifecycle : NutViewLifecycle, INutIosViewLifecycle
    {
        private readonly INutSerializer serializer;
        private readonly INutViewModelContainer viewModelContainer;

        public NutIosViewLifecycle(INutSerializer serializer,
                                   INutViewModelContainer viewModelContainer) : base(viewModelContainer)
        {
            this.serializer = serializer;
            this.viewModelContainer = viewModelContainer;
        }

        public override void OnAfterCreate(INutView view, Type modelType)
        {
            base.OnAfterCreate(view, modelType);

            var viewModelParams = CreateViewModelParameters(view);
            var viewModel = viewModelContainer.GetOrCreateViewModel(view.Identifier, modelType, viewModelParams);
            view.BindingContext.DataSource = viewModel;
        }

        public override void OnBeforeDestroy(INutView view)
        {
            if (IsViewWillBeDestroyed(view))
            {
                base.OnBeforeDestroy(view);
            }
        }

        public override void OnAfterDestroy(INutView view)
        {
            if (IsViewWillBeDestroyed(view))
            {
                base.OnAfterDestroy(view);
            }
        }

        protected virtual bool IsViewWillBeDestroyed(INutView view)
        {
            var controller = view as UIViewController;
            if (controller == null)
            {
                return true;
            }

            if (controller.NavigationController == null && controller.IsMovingFromParentViewController)
            {
                return true;
            }

            if (controller.ParentViewController != null && controller.ParentViewController.IsBeingDismissed)
            {
                return true;
            }

            return false;
        }

        private string CreateViewModelParameters(INutView view)
        {
            var nutIosView = view as INutIosView;
            var viewModelParameters = nutIosView?.ViewModelParameters;
            if (viewModelParameters == null)
            {
                return null;
            }

            var parametersType = viewModelParameters.GetType();
            if (parametersType.IsValueType || viewModelParameters is string)
            {
                return viewModelParameters.ToString();
            }

            return serializer.Serialize(viewModelParameters);
        }
    }
}