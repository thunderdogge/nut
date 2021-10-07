using Android.Content;
using Android.Views;
using Android.Widget;
using Nut.Core;
using Nut.Core.Application;
using Nut.Core.Bindings;
using Nut.Droid.Bindings;
using Nut.Droid.Views;
using Nut.Ioc;

namespace Nut.Droid
{
    public abstract class NutDroidSetup : NutApplicationSetup
    {
        private readonly Context context;

        protected NutDroidSetup(Context context)
        {
            this.context = context;
        }

        protected override void SetupIoC()
        {
            base.SetupIoC();

            var serviceDescriptions = typeof(NutDroidSetup).GetAssemblyServiceDescriptions();
            Nuts.RegisterServiceDescriptions(serviceDescriptions);
        }

        protected override INutTargetBindingContainer SetupBindingContainer()
        {
            var container = base.SetupBindingContainer();

            container.RegisterTargetBindingFactory("Tap", new NutTargetBindingFactory<View>(x => new NutDroidTapTargetBinding(x)));
            container.RegisterTargetBindingFactory("Text", new NutTargetBindingFactory<TextView>(x => new NutDroidTextTargetBinding(x)));
            container.RegisterTargetBindingFactory("Blur", new NutTargetBindingFactory<EditText>(x => new NutDroidBlurTargetBinding(x)));
            container.RegisterTargetBindingFactory("Image", new NutTargetBindingFactory<ImageView>(x => new NutDroidImageTargetBinding(x)));
            container.RegisterTargetBindingFactory("Hidden", new NutTargetBindingFactory<View>(x => new NutDroidHiddenTargetBinding(x)));
            container.RegisterTargetBindingFactory("Checked", new NutTargetBindingFactory<CheckBox>(x => new NutDroidCheckedTargetBinding(x)));
            container.RegisterTargetBindingFactory("EditDone", new NutTargetBindingFactory<EditText>(x => new NutDroidEditDoneTargetBinding(x)));
            container.RegisterTargetBindingFactory("TextColor", new NutTargetBindingFactory<TextView>(x => new NutDroidTextColorTargetBinding(x)));
            container.RegisterTargetBindingFactory("Validation", new NutTargetBindingFactory<View>(x => new NutDroidValidationTargetBinding(x)));
            container.RegisterTargetBindingFactory("Visibility", new NutTargetBindingFactory<View>(x => new NutDroidVisibilityTargetBinding(x)));
            container.RegisterTargetBindingFactory("BackgroundColor", new NutTargetBindingFactory<View>(x => new NutDroidBackgroundColorTargetBinding(x)));

            return container;
        }

        protected override void SetupComplete()
        {
            base.SetupComplete();

            var viewMonitor = Nuts.Resolve<INutDroidViewMonitor>();
            viewMonitor.CurrentContext = context;
        }
    }
}