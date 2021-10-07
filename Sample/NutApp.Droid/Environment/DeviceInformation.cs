using Android.OS;
using NutApp.Core.Environment;

namespace NutApp.Droid.Environment
{
    public class DeviceInformation : IDeviceInformation
    {
        public DeviceInformation()
        {
            AppVersion = Package.Version.Number;
            Device = $"{Build.Manufacturer} {Build.Model}";
            Software = $"{Build.VERSION.Release} (SDK {Build.VERSION.Sdk})";
        }

        public int AppVersion { get; }
        public string Device { get; }
        public string Software { get; }
    }
}