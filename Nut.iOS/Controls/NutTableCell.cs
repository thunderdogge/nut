using System;
using Nut.Core.Bindings;
using UIKit;

namespace Nut.iOS.Controls
{
    public abstract class NutTableCell : UITableViewCell, INutTableCell
    {
        private INutBindingContext bindingContext;

        protected NutTableCell(IntPtr handle) : base(handle)
        {
        }

        public object DataSource
        {
            get { return BindingContext.DataSource; }
            set
            {
                var currentSource = BindingContext.DataSource;
                if (currentSource == value)
                {
                    return;
                }

                if (currentSource != null)
                {
                    BindingContext.UnregisterBindings();
                }

                BindingContext.DataSource = value;
                ConfigureBindings();
            }
        }

        public INutBindingContext BindingContext
        {
            get { return bindingContext ?? (bindingContext = new NutBindingContext()); }
            set { bindingContext = value; }
        }

        protected abstract void ConfigureBindings();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (bindingContext != null)
                {
                    bindingContext.UnregisterBindings();
                }
            }

            base.Dispose(disposing);
        }
    }
}