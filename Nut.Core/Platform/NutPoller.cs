using System;
using System.Threading.Tasks;

namespace Nut.Core.Platform
{
    public class NutPoller
    {
        public static IDisposable Run(Action action, TimeSpan interval, bool delayRun = false)
        {
            var isRunning = true;
            Task.Run(async () =>
            {
                while (isRunning)
                {
                    if (!delayRun)
                    {
                        action();
                    }

                    await Task.Delay(interval);

                    if (delayRun)
                    {
                        action();
                    }
                }
            });

            return new NutDisposableCallback(() => isRunning = false);
        }
    }
}