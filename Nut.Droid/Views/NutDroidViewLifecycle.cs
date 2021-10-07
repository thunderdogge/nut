using System;
using Android.App;
using Android.Content;
using Android.OS;
using Nut.Core.Models;
using Nut.Core.Serialization;
using Nut.Core.Views;
using Nut.Droid.Extensions;
using Nut.Droid.Platform;
using Fragment = Android.Support.V4.App.Fragment;

namespace Nut.Droid.Views
{
    public class NutDroidViewLifecycle : NutViewLifecycle, INutDroidViewLifecycle
    {
        private readonly INutSerializer serializer;
        private readonly INutDroidViewMonitor viewMonitor;
        private readonly INutViewModelContainer viewModelContainer;

        public NutDroidViewLifecycle(INutSerializer serializer,
                                     INutDroidViewMonitor viewMonitor,
                                     INutViewModelContainer viewModelContainer) : base(viewModelContainer)
        {
            this.serializer = serializer;
            this.viewMonitor = viewMonitor;
            this.viewModelContainer = viewModelContainer;
        }

        public void OnBeforeCreate(INutView view, Type modelType, Bundle bundle)
        {
            base.OnBeforeCreate(view, modelType);

            view.Identifier = bundle?.GetGuid(NutDroidConsts.ActivityKey) ?? Guid.NewGuid();
            viewMonitor.CurrentContext = GetContextFromView(view);
        }

        public void OnAfterCreate(INutView view, Type modelType, Bundle bundle)
        {
            base.OnAfterCreate(view, modelType);

            var initParams = bundle?.GetString(NutDroidConsts.IntentParametersKey);
            var viewModel = viewModelContainer.GetOrCreateViewModel(view.Identifier, modelType, initParams);
            viewModel.State = initParams;
            view.BindingContext.DataSource = viewModel;
        }

        public override void OnBeforeResume(INutView view)
        {
            base.OnBeforeResume(view);
            viewMonitor.CurrentContext = GetContextFromView(view);
        }

        public void OnBeforeSaveInstanceState(INutView view, Bundle bundle)
        {
            bundle.PutGuid(NutDroidConsts.ActivityKey, view.Identifier);
        }

        public void OnAfterSaveInstanceState(INutView view, Bundle bundle)
        {
            var viewModel = view.BindingContext.DataSource as INutViewModel;
            var viewModelState = viewModel?.State;
            if (viewModelState == null)
            {
                return;
            }

            string bundleValue;
            var stateType = viewModelState.GetType();
            if (stateType.IsValueType || viewModelState is string)
            {
                bundleValue = viewModelState.ToString();
            }
            else
            {
                bundleValue = serializer.Serialize(viewModelState);
            }

            bundle.PutString(NutDroidConsts.IntentParametersKey, bundleValue);
        }

        protected override bool IsViewDestroyed(INutView view)
        {
            var activity = view as Activity;
            return base.IsViewDestroyed(view) && activity != null && activity.IsFinishing;
        }

        private static Context GetContextFromView(INutView view)
        {
            var fragment = view as Fragment;
            if (fragment != null)
            {
                return fragment.Activity;
            }

            return view as Context;
        }
    }
}