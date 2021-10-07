using Nut.Core.Application;
using Nut.Core.Platform;

namespace Nut.Core.Extensions
{
    public static class NutApplicationExtensions
    {
        public static void Launch(this INutApplicationEntry entry)
        {
            var launcher = NutSingleton<INutApplicationLauncher>.Instance ?? entry.CreateLauncher();
            launcher.Launch();
        }

        public static void EnsureReady(this INutApplicationEntry entry)
        {
            var launcher = NutSingleton<INutApplicationLauncher>.Instance ?? entry.CreateLauncher();
            launcher.Warmup();
        }
    }
}