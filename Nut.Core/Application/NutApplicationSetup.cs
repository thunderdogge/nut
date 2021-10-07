using Nut.Core.Views;
using Nut.Core.Platform;
using Nut.Core.Bindings;
using Nut.Core.Bindings.Converters;
using Nut.Ioc;

namespace Nut.Core.Application
{
    [NutIocIgnore]
    public abstract class NutApplicationSetup : NutSingleton<INutApplicationSetup>, INutApplicationSetup
    {
        public void Setup()
        {
            SetupStart();
            SetupPrimary();
            SetupSecondary();
            SetupComplete();
        }

        protected virtual void SetupStart()
        {
            // Empty
        }

        private void SetupPrimary()
        {
            SetupIoC();
        }

        private void SetupSecondary()
        {
            SetupViewMapper();
            SetupBindingContainer();
            SetupBindingConverters();
        }

        protected virtual void SetupComplete()
        {
            // Empty
        }

        protected virtual void SetupIoC()
        {
        }

        protected virtual INutViewMapper SetupViewMapper()
        {
            return Nuts.Resolve<INutViewMapper>();
        }

        protected virtual INutTargetBindingContainer SetupBindingContainer()
        {
            var container = Nuts.Resolve<INutTargetBindingContainer>();
            container.RegisterTargetBindingFactory("Source", new NutTargetBindingFactory<INutCollectionSource>(x => new NutSourceTargetBinding(x)));
            container.RegisterTargetBindingFactory("SourceSelect", new NutTargetBindingFactory<INutCollectionSource>(x => new NutSourceSelectTargetBinding(x)));

            return container;
        }

        protected virtual INutTargetBindingConverterContainer SetupBindingConverters()
        {
            var container = Nuts.Resolve<INutTargetBindingConverterContainer>();
            container.RegisterBindingConverter("InvertedBool", new NutTargetBindingInvertedBoolConverter());

            return container;
        }
    }
}