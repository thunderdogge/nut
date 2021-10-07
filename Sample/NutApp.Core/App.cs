using Nut.Core;
using Nut.Core.Application;
using Nut.Ioc;

namespace NutApp.Core
{
    public class App : NutApplication
    {
        public App(INutApplicationSetup setup) : base(setup)
        {
        }

        protected override void SetupIoC()
        {
            base.SetupIoC();

            var serviceDescriptions = typeof(App).GetAssemblyServiceDescriptions();
            Nuts.RegisterServiceDescriptions(serviceDescriptions);
        }
    }
}