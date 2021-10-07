using Nut.Core.Application;
using Nut.Core.Logging;
using Nut.Droid.Logging;

namespace Nut.Droid
{
    public abstract class NutDroidLauncher : NutApplicationLauncher
    {
        protected override INutLogger CreateLogger()
        {
            return new NutDroidLogger();
        }
    }
}