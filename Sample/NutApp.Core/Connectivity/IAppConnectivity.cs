using System;

namespace NutApp.Core.Connectivity
{
    public interface IAppConnectivity
    {
        bool IsConnected { get; }
        bool IsDisconnected { get; }
        event Action ConnectionAppeared;
        event Action ConnectionDisappeared;
    }
}