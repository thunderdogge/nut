using Nut.Droid.Screens;
using Nut.Droid.Views;
using NutApp.Core.Screens.Navigation;

namespace NutApp.Droid.Components
{
    public class ScreenNavigator : NutDroidScreenNavigator, IScreenNavigator
    {
        public ScreenNavigator(INutDroidViewMonitor viewMonitor) : base(viewMonitor)
        {
        }
    }
}