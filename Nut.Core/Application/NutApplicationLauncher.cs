using Nut.Core.Logging;
using Nut.Core.Platform;
using Nut.Ioc;

namespace Nut.Core.Application
{
    [NutIocIgnore]
    public abstract class NutApplicationLauncher : NutSingleton<INutApplicationLauncher>, INutApplicationLauncher
    {
        public void Launch()
        {
            var logger = GetOrCreateLogger();
            Nuts.Info("Hello world!");

            var application = GetOrCreateApplication();
            application.Setup();
            application.Start();
        }

        public void Warmup()
        {
            var logger = GetOrCreateLogger();
            var application = GetOrCreateApplication();
            application.Setup();
        }

        private INutLogger GetOrCreateLogger()
        {
            return NutSingleton<INutLogger>.Instance ?? CreateLogger();
        }

        private INutApplication GetOrCreateApplication()
        {
            return NutSingleton<INutApplication>.Instance ?? CreateApplication();
        }

        protected abstract INutLogger CreateLogger();

        protected abstract INutApplication CreateApplication();
    }
}