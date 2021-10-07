using System;
using Nut.Core.Models;

namespace Nut.Core.Views
{
    public abstract class NutViewLifecycle : INutViewLifecycle
    {
        private readonly INutViewModelContainer viewModelContainer;

        protected NutViewLifecycle(INutViewModelContainer viewModelContainer)
        {
            this.viewModelContainer = viewModelContainer;
        }

        public virtual void OnBeforeCreate(INutView view, Type modelType)
        {
        }

        public virtual void OnAfterCreate(INutView view, Type modelType)
        {
            view.BindingContext.UnregisterBindings();
        }

        public virtual void OnBeforeResume(INutView view)
        {
        }

        public virtual void OnAfterResume(INutView view)
        {
            var viewModel = view.BindingContext.DataSource as INutViewModel;
            viewModel?.Resume();
        }

        public virtual void OnBeforePause(INutView view)
        {
        }

        public virtual void OnAfterPause(INutView view)
        {
            var viewModel = view.BindingContext.DataSource as INutViewModel;
            viewModel?.Pause();
        }

        public virtual void OnBeforeStop(INutView view)
        {
        }

        public virtual void OnAfterStop(INutView view)
        {
            var viewModel = view.BindingContext.DataSource as INutViewModel;
            viewModel?.Stop();
        }

        public virtual void OnBeforeDestroy(INutView view)
        {
        }

        public virtual void OnAfterDestroy(INutView view)
        {
            view.BindingContext.UnregisterBindings();

            var actuallyDestroyed = IsViewDestroyed(view);
            if (actuallyDestroyed)
            {
                viewModelContainer.RemoveViewModel(view.Identifier);

                var viewModel = view.BindingContext.DataSource as INutViewModel;
                viewModel?.Finish();
            }
        }

        protected virtual bool IsViewDestroyed(INutView view)
        {
            return true;
        }
    }
}