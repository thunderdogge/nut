using NutApp.Core.Storage;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;

namespace NutApp.iOS.Storage
{
    public class EntityStorageSettings : IEntityStorageSettings
    {
        public EntityStorageSettings()
        {
            Path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "database.db3");
            Platform = new SQLitePlatformIOS();
        }

        public string Path { get; }
        public ISQLitePlatform Platform { get; }
    }
}