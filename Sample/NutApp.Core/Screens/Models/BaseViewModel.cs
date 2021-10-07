using System.Threading.Tasks;
using Nut.Core;
using Nut.Core.Models;
using NutApp.Core.Components;
using NutApp.Core.Connectivity;

namespace NutApp.Core.Screens.Models
{
    public abstract class BaseViewModel : NutViewModel
    {
        private bool isEmpty;
        private bool isLoading;

        public bool IsEmpty
        {
            get { return isEmpty; }
            set { isEmpty = value; RaisePropertyChanged(); }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; RaisePropertyChanged(); }
        }

        public async Task RefreshAsync()
        {
            var appConnectivity = Nuts.Resolve<IAppConnectivity>();
            if (appConnectivity.IsDisconnected)
            {
                var notifier = Nuts.Resolve<INotifier>();
                notifier.NotifyLong("Check your internet connection");

                return;
            }

            await Task.Delay(1000);
        }
    }
}