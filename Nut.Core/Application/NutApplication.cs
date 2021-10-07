using Nut.Core.Dependencies;
using Nut.Core.Models.Validation;
using Nut.Core.Platform;
using Nut.Ioc;

namespace Nut.Core.Application
{
    [NutIocIgnore]
    public class NutApplication : NutSingleton<INutApplication>, INutApplication
    {
        private readonly INutApplicationSetup setup;
        private static bool setupCompleted;

        public NutApplication(INutApplicationSetup setup)
        {
            this.setup = setup;
        }

        public void Setup()
        {
            if (setupCompleted)
            {
                return;
            }

            using (Nuts.Trace("App setup"))
            {
                SetupIoC();
                SetupApp();
            }

            setupCompleted = true;
        }

        protected virtual void SetupApp()
        {
            setup.Setup();
        }

        protected virtual void SetupIoC()
        {
            var iocProvider = CreateIocProvider();
            iocProvider.RegisterSingleton<INutIocProvider>(iocProvider);

            var serviceDescriptions = typeof(Nuts).GetAssemblyServiceDescriptions();
            iocProvider.RegisterServiceDescriptions(serviceDescriptions);

            iocProvider.RegisterUnique<INutValidator, NutValidator>();
        }

        protected virtual NutIocProvider CreateIocProvider()
        {
            return new NutIocProvider();
        }

        public void Start()
        {
            using (Nuts.Trace("App start"))
            {
                Nuts.Resolve<INutApplicationStart>().Start();
            }
        }
    }
}