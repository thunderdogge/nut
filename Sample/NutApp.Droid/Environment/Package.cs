using Android.App;
using Android.Content.PM;

namespace NutApp.Droid.Environment
{
    public static class Package
    {
        static Package()
        {
            Info = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0);
            Version = new PackageVersion { Name = Info.VersionName, Number = Info.VersionCode };
        }

        public static PackageInfo Info { get; }
        public static PackageVersion Version { get; }
    }

    public class PackageVersion
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }
}