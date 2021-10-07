using Nut.Core.Application;
using Nut.iOS;
using NutApp.Core;
using UIKit;

namespace NutApp.iOS
{
    public class Launcher : NutIosLauncher
    {
        private readonly UIWindow window;

        public Launcher(UIWindow window)
        {
            this.window = window;
        }

        protected override INutApplication CreateApplication()
        {
            return new App(new Setup(window));
        }
    }
}