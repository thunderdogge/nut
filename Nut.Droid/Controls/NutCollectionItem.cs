using Android.Views;
using Nut.Core.Bindings;

namespace Nut.Droid.Controls
{
    public abstract class NutCollectionItem : INutCollectionItem
    {
        private INutBindingContext bindingContext;

        protected NutCollectionItem(View itemView)
        {
            ItemView = itemView;
        }

        public View ItemView { get; set; }

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

        public void Dispose()
        {
            if (bindingContext != null)
            {
                bindingContext.UnregisterBindings();
            }
        }
    }
}