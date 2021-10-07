using System;
using Nut.iOS.Components;
using NutApp.Core.Components;

namespace NutApp.iOS.Components
{
    public class Toaster : IToaster
    {
        private static readonly nint longDelaySeconds = new nint(1200);
        private static readonly nint shortDelaySeconds = new nint(1000);

        public void ShowLong(string message)
        {
            Show(message, longDelaySeconds);
        }

        public void ShowShort(string message)
        {
            Show(message, shortDelaySeconds);
        }

        private static void Show(string message, nint delay)
        {
            NutToast.MakeText(message, delay);
        }
    }
}