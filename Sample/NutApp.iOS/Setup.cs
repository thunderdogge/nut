using Nut.Core;
using Nut.Core.Views;
using Nut.iOS;
using Nut.Ioc;
using NutApp.Core.Screens.Models;
using NutApp.iOS.Screens;
using UIKit;

namespace NutApp.iOS
{
    public class Setup : NutIosSetup
    {
        public Setup(UIWindow window) : base(window)
        {
        }

        protected override void SetupIoC()
        {
            base.SetupIoC();

            var serviceDescriptions = typeof(Setup).GetAssemblyServiceDescriptions();
            Nuts.RegisterServiceDescriptions(serviceDescriptions);
        }

        protected override INutViewMapper SetupViewMapper()
        {
            var viewMapper = base.SetupViewMapper();
            viewMapper.Map<DashboardViewModel, DashboardViewController>();
            viewMapper.Map<ReminderModifyViewModel, ReminderModifyViewController>();

            return viewMapper;
        }
    }
}