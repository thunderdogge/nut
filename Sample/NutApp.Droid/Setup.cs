using Android.Content;
using Nut.Core;
using Nut.Core.Views;
using Nut.Droid;
using Nut.Ioc;
using NutApp.Core.Screens.Models;
using NutApp.Droid.Screens;

namespace NutApp.Droid
{
    public class Setup : NutDroidSetup
    {
        public Setup(Context context) : base(context)
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
            viewMapper.Map<DashboardViewModel, DashboardActivity>();
            viewMapper.Map<ReminderModifyViewModel, ReminderModifyActivity>();

            return viewMapper;
        }
    }
}