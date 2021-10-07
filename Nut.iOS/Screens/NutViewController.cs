using System;
using Nut.Core;
using Nut.Core.Bindings;
using Nut.Core.Models;
using Nut.iOS.Views;
using Nut.Ioc;
using UIKit;

namespace Nut.iOS.Screens
{
    [NutIocIgnore]
    public abstract class NutViewController<TViewModel> : UIViewController, INutIosView where TViewModel : class , INutViewModel
    {
        private readonly INutIosViewLifecycle viewLifecycle;
        private INutBindingContext bindingContext;
        private Guid? id;

        protected NutViewController(string nibName) : base(nibName, null)
        {
            viewLifecycle = Nuts.Resolve<INutIosViewLifecycle>();
        }

        public Guid Identifier
        {
            get { return (Guid) (id ?? (id = Guid.NewGuid())); }
            set { id = value; }
        }

        public TViewModel ViewModel
        {
            get { return BindingContext.DataSource as TViewModel; }
            set { BindingContext.DataSource = value; }
        }

        public object ViewModelParameters { get; set; }

        public INutBindingContext BindingContext
        {
            get { return bindingContext ?? (bindingContext = new NutBindingContext()); }
            set { bindingContext = value; }
        }

        public override void ViewDidLoad()
        {
            viewLifecycle.OnBeforeCreate(this, typeof(TViewModel));
            base.ViewDidLoad();
            viewLifecycle.OnAfterCreate(this, typeof(TViewModel));
        }

        public override void ViewWillAppear(bool animated)
        {
            viewLifecycle.OnBeforeResume(this);
            base.ViewWillAppear(animated);
            viewLifecycle.OnAfterResume(this);
        }

        public override void ViewDidDisappear(bool animated)
        {
            viewLifecycle.OnBeforePause(this);
            viewLifecycle.OnBeforeStop(this);
            viewLifecycle.OnBeforeDestroy(this);
            base.ViewDidDisappear(animated);
            viewLifecycle.OnAfterPause(this);
            viewLifecycle.OnAfterStop(this);
            viewLifecycle.OnAfterDestroy(this);
        }
    }
}