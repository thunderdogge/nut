using System;
using Android.OS;
using Android.Support.V7.App;
using Nut.Core;
using Nut.Core.Application;
using Nut.Core.Bindings;
using Nut.Core.Extensions;
using Nut.Core.Models;
using Nut.Core.Views;
using Nut.Droid.Views;

namespace Nut.Droid.Screens
{
    public abstract class NutActivity<TViewModel> : AppCompatActivity, INutApplicationEntry, INutView where TViewModel : class, INutViewModel
    {
        private INutBindingContext bindingContext;
        private readonly INutDroidViewLifecycle viewLifecycle;

        protected NutActivity()
        {
            this.EnsureReady();

            viewLifecycle = Nuts.Resolve<INutDroidViewLifecycle>();
        }

        public Guid Identifier { get; set; }

        public TViewModel ViewModel
        {
            get { return BindingContext.DataSource as TViewModel; }
            set { BindingContext.DataSource = value; }
        }

        public INutBindingContext BindingContext
        {
            get { return bindingContext ?? (bindingContext = new NutBindingContext()); }
            set { bindingContext = value; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            viewLifecycle.OnBeforeCreate(this, typeof(TViewModel), bundle ?? Intent.Extras);
            base.OnCreate(bundle);
            viewLifecycle.OnAfterCreate(this, typeof(TViewModel), bundle ?? Intent.Extras);
        }

        protected override void OnResume()
        {
            viewLifecycle.OnBeforeResume(this);
            base.OnResume();
            viewLifecycle.OnAfterResume(this);
        }

        protected override void OnPause()
        {
            viewLifecycle.OnBeforePause(this);
            base.OnPause();
            viewLifecycle.OnAfterPause(this);
        }

        protected override void OnStop()
        {
            viewLifecycle.OnBeforeStop(this);
            base.OnStop();
            viewLifecycle.OnAfterStop(this);
        }

        protected override void OnDestroy()
        {
            viewLifecycle.OnBeforeDestroy(this);
            base.OnDestroy();
            viewLifecycle.OnAfterDestroy(this);
        }

        protected override void OnSaveInstanceState(Bundle bundle)
        {
            viewLifecycle.OnBeforeSaveInstanceState(this, bundle ?? Intent.Extras);
            base.OnSaveInstanceState(bundle);
            viewLifecycle.OnAfterSaveInstanceState(this, bundle ?? Intent.Extras);
        }

        public abstract INutApplicationLauncher CreateLauncher();
    }
}