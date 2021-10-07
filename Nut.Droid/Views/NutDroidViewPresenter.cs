using System;
using Android.App;
using Android.Content;
using Android.OS;
using Nut.Core.Dependencies;
using Nut.Core.Models;
using Nut.Core.Navigation;
using Nut.Core.Platform;
using Nut.Core.Serialization;
using Nut.Core.Views;
using Nut.Droid.Platform;
using Fragment = Android.Support.V4.App.Fragment;

namespace Nut.Droid.Views
{
    public class NutDroidViewPresenter : NutViewPresenter
    {
        private readonly INutSerializer serializer;
        private readonly INutViewMapper viewMapper;
        private readonly INutIocProvider iocProvider;
        private readonly INutDroidViewMonitor viewMonitor;

        public NutDroidViewPresenter(INutSerializer serializer,
                                     INutViewMapper viewMapper,
                                     INutIocProvider iocProvider,
                                     INutDroidViewMonitor viewMonitor,
                                     INutMainThreadDispatcher dispatcher) : base(dispatcher)
        {
            this.serializer = serializer;
            this.viewMapper = viewMapper;
            this.iocProvider = iocProvider;
            this.viewMonitor = viewMonitor;
        }

        protected override void ShowViewModel(NutViewModelRequest viewModelRequest)
        {
            var context = viewMonitor.CurrentContext;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "Current context is missing");
            }

            var viewType = viewMapper.GetViewType(viewModelRequest.ViewModelType);
            ShowViewModel(context, viewType, viewModelRequest);
        }

        protected virtual void ShowViewModel(Context context, Type viewType, NutViewModelRequest viewModelRequest)
        {
            if (typeof(Fragment).IsAssignableFrom(viewType))
            {
                ShowFragmentViewModel(context, viewType, viewModelRequest);
                return;
            }

            ShowActivityViewModel(context, viewType, viewModelRequest);
        }

        protected override void CloseViewModel(INutViewModel viewModel)
        {
            var activity = viewMonitor.CurrentActivity;
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity), "Attempt to close ViewModel with null activity");
            }

            activity.Finish();
        }

        private void ShowFragmentViewModel(Context context, Type viewType, NutViewModelRequest viewModelRequest)
        {
            var fragment = iocProvider.Create(viewType) as Fragment;
            if (fragment == null)
            {
                throw new ArgumentNullException(nameof(fragment), "Attempt to create fragment instance failed");
            }

            var fragmentedView = context as INutDroidFragmentedView;
            if (fragmentedView == null)
            {
                throw new ArgumentNullException(nameof(context), $"Context must implement `{nameof(INutDroidFragmentedView)}`");
            }

            var arguments = CreateIntentExtras(viewModelRequest);
            fragment.Arguments = arguments;
            fragmentedView.ShowFragment(fragment);
        }

        private void ShowActivityViewModel(Context context, Type viewType, NutViewModelRequest viewModelRequest)
        {
            var intent = CreateIntent(context, viewType, viewModelRequest);
            context.StartActivity(intent);
        }

        private Intent CreateIntent(Context context, Type viewType, NutViewModelRequest viewModelRequest)
        {
            var intentFlags = CreateIntentFlags(context, viewModelRequest.Mode);
            var intentExtras = CreateIntentExtras(viewModelRequest);

            var intent = new Intent(context, viewType);
            intent.AddFlags(intentFlags);
            intent.PutExtras(intentExtras);

            return intent;
        }

        private Bundle CreateIntentExtras(NutViewModelRequest viewModelRequest)
        {
            var bundle = new Bundle();
            var extra = CreateIntentExtrasValue(viewModelRequest.ViewModelParameters);
            bundle.PutString(NutDroidConsts.IntentParametersKey, extra);

            return bundle;
        }

        private string CreateIntentExtrasValue(object viewModelParameters)
        {
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

        private static ActivityFlags CreateIntentFlags(Context context, NutViewModelRequestMode mode)
        {
            var flags = context is Activity ? 0 : ActivityFlags.NewTask;

            if ((mode & NutViewModelRequestMode.NewTask) != 0)
            {
                flags |= ActivityFlags.NewTask;
            }
            if ((mode & NutViewModelRequestMode.NewDocument) != 0)
            {
                flags |= ActivityFlags.NewDocument;
            }
            if ((mode & NutViewModelRequestMode.MultipleTask) != 0)
            {
                flags |= ActivityFlags.MultipleTask;
            }
            if ((mode & NutViewModelRequestMode.SingleTop) != 0)
            {
                flags |= ActivityFlags.SingleTop;
            }
            if ((mode & NutViewModelRequestMode.ClearTop) != 0)
            {
                flags |= ActivityFlags.ClearTop;
            }
            if ((mode & NutViewModelRequestMode.ClearStack) != 0)
            {
                flags |= ActivityFlags.ClearTask;
            }
            if ((mode & NutViewModelRequestMode.NoHistory) != 0)
            {
                flags |= ActivityFlags.NoHistory;
            }
            if ((mode & NutViewModelRequestMode.NoAnimation) != 0)
            {
                flags |= ActivityFlags.NoAnimation;
            }

            return flags;
        }
    }
}