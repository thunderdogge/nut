namespace Nut.Core.Environment
{
    public interface INutPreferences
    {
        bool Contains(string key);
        void PutInt(string key, int value);
        int GetInt(string key, int defaults = 0);
        void PutLong(string key, long value);
        long GetLong(string key, long defaults = 0);
        void PutBoolean(string key, bool value);
        bool GetBoolean(string key, bool defaults);
        void PutString(string key, string value);
        string GetString(string key, string defaults = null);
        void Remove(string key);
        void Clear();
    }
}