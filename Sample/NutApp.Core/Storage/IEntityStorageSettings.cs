using SQLite.Net.Interop;

namespace NutApp.Core.Storage
{
    public interface IEntityStorageSettings
    {
        string Path { get; }
        ISQLitePlatform Platform { get; }
    }
}