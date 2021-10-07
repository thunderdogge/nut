using System;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace NutApp.Core.Connectivity
{
    public class AppConnectivity : IAppConnectivity
    {
        private bool subscriptionAttached;

        public bool IsConnected => CrossConnectivity.Current.IsConnected;

        public bool IsDisconnected => !IsConnected;

        private event Action ConnectionAppeared;
        event Action IAppConnectivity.ConnectionAppeared
        {
            add
            {
                SubscribeConnectivityChanged();
                ConnectionAppeared += value;
            }
            remove { ConnectionAppeared -= value; }
        }

        private event Action ConnectionDisappeared;
        event Action IAppConnectivity.ConnectionDisappeared
        {
            add
            {
                SubscribeConnectivityChanged();
                ConnectionDisappeared += value;
            }
            remove { ConnectionDisappeared -= value; }
        }

        private void SubscribeConnectivityChanged()
        {
            if (subscriptionAttached)
            {
                return;
            }

            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
            subscriptionAttached = true;
        }

        private void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs args)
        {
            if (args.IsConnected)
            {
                ConnectionAppeared?.Invoke();
            }
            else
            {
                ConnectionDisappeared?.Invoke();
            }
        }
    }
}