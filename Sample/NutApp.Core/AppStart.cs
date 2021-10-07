using Nut.Core.Application;
using NutApp.Core.Screens.Models;

namespace NutApp.Core
{
    public class AppStart : NutApplicationStart
    {
        public override void Start()
        {
            ShowViewModel<DashboardViewModel>();
        }
    }
}