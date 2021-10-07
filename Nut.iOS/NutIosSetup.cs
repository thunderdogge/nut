using Foundation;
using Nut.Core;
using Nut.Core.Application;
using Nut.Core.Bindings;
using Nut.iOS.Bindings;
using Nut.iOS.Views;
using Nut.Ioc;
using UIKit;

namespace Nut.iOS
{
    public class NutIosSetup : NutApplicationSetup
    {
        private readonly UIWindow window;

        public NutIosSetup(UIWindow window)
        {
            this.window = window;
        }

        protected override void SetupIoC()
        {
            base.SetupIoC();

            var serviceDescriptions = typeof(NutIosSetup).GetAssemblyServiceDescriptions();
            Nuts.RegisterServiceDescriptions(serviceDescriptions);
        }

        protected override INutTargetBindingContainer SetupBindingContainer()
        {
            var container = base.SetupBindingContainer();

            container.RegisterTargetBindingFactory("Tap", new NutTargetBindingFactory<NSObject>(x => new NutIosTapTargetBinding(x)));
            container.RegisterTargetBindingFactory("Text", new NutTargetBindingFactory<UIView>(x => new NutIosTextTargetBinding(x)));
            container.RegisterTargetBindingFactory("Image", new NutTargetBindingFactory<UIImageView>(x => new NutIosImageTargetBinding(x)));
            container.RegisterTargetBindingFactory("Hidden", new NutTargetBindingFactory<UIView>(x => new NutIosHiddenTargetBinding(x)));
            container.RegisterTargetBindingFactory("EditDone", new NutTargetBindingFactory<UITextField>(x => new NutIosEditDoneTargetBinding(x)));
            container.RegisterTargetBindingFactory("TextColor", new NutTargetBindingFactory<UIView>(x => new NutIosTextColorTargetBinding(x)));
            container.RegisterTargetBindingFactory("Visibility", new NutTargetBindingFactory<UIView>(x => new NutIosVisibilityTargetBinding(x)));
            container.RegisterTargetBindingFactory("BackgroundColor", new NutTargetBindingFactory<UIView>(x => new NutIosBackgroundColorTargetBinding(x)));

            return container;
        }

        protected override void SetupComplete()
        {
            base.SetupComplete();

            var viewMonitor = Nuts.Resolve<INutIosViewMonitor>();
            viewMonitor.Window = window;
        }
    }
}