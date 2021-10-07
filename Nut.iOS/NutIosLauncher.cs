using Nut.Core.Application;
using Nut.Core.Logging;
using Nut.iOS.Logging;

namespace Nut.iOS
{
    public abstract class NutIosLauncher : NutApplicationLauncher
    {
        protected override INutLogger CreateLogger()
        {
            return new NutIosLogger();
        }
    }
}