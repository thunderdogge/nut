using NutApp.Core.Storage;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using E = System.Environment;
using P = System.IO.Path;

namespace NutApp.Droid.Storage
{
    public class EntityStorageSettings : IEntityStorageSettings
    {
        public EntityStorageSettings()
        {
            Path = P.Combine(E.GetFolderPath(E.SpecialFolder.Personal), "database.db3");
            Platform = new SQLitePlatformAndroid();
        }

        public string Path { get; }
        public ISQLitePlatform Platform { get; }
    }
}