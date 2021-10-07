using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Nut.Core;
using Nut.Core.Bindings;
using Nut.Core.Models;
using Nut.Core.Views;
using Nut.Droid.Views;

namespace Nut.Droid.Controls
{
    public abstract class NutFragment<TViewModel> : Fragment, INutView where TViewModel : class, INutViewModel
    {
        private INutBindingContext bindingContext;
        private readonly INutDroidViewLifecycle viewLifecycle;

        protected NutFragment()
        {
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

        public override void OnViewCreated(View view, Bundle bundle)
        {
            viewLifecycle.OnBeforeCreate(this, typeof(TViewModel), bundle ?? Arguments);
            base.OnViewCreated(view, bundle);
            viewLifecycle.OnAfterCreate(this, typeof(TViewModel), bundle ?? Arguments);
        }

        public override void OnResume()
        {
            viewLifecycle.OnBeforeResume(this);
            base.OnResume();
            viewLifecycle.OnAfterResume(this);
        }

        public override void OnPause()
        {
            viewLifecycle.OnBeforePause(this);
            base.OnPause();
            viewLifecycle.OnAfterPause(this);
        }

        public override void OnStop()
        {
            viewLifecycle.OnBeforeStop(this);
            base.OnStop();
            viewLifecycle.OnAfterStop(this);
        }

        public override void OnDestroy()
        {
            viewLifecycle.OnBeforeDestroy(this);
            base.OnDestroy();
            viewLifecycle.OnAfterDestroy(this);
        }

        public override void OnSaveInstanceState(Bundle bundle)
        {
            viewLifecycle.OnBeforeSaveInstanceState(this, bundle ?? Arguments);
            base.OnSaveInstanceState(bundle);
            viewLifecycle.OnAfterSaveInstanceState(this, bundle ?? Arguments);
        }
    }
}