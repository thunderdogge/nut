using Android.App;
using Android.Content;
using Nut.Core.Environment;

namespace Nut.Droid.Environment
{
    public abstract class NutDroidPreferences : INutPreferences
    {
        private readonly ISharedPreferences scope;

        protected NutDroidPreferences(string name) : this(name, FileCreationMode.Private)
        {
        }

        protected NutDroidPreferences(string name, FileCreationMode fileCreationMode)
        {
            scope = Application.Context.GetSharedPreferences(name, fileCreationMode);
        }

        public bool Contains(string key)
        {
            return scope.Contains(key);
        }

        public void PutInt(string key, int value)
        {
            scope.Edit().PutInt(key, value).Apply();
        }

        public int GetInt(string key, int defaults = 0)
        {
            return scope.GetInt(key, defaults);
        }

        public void PutLong(string key, long value)
        {
            scope.Edit().PutLong(key, value).Apply();
        }

        public long GetLong(string key, long defaults = 0)
        {
            return scope.GetLong(key, defaults);
        }

        public void PutBoolean(string key, bool value)
        {
            scope.Edit().PutBoolean(key, value).Apply();
        }

        public bool GetBoolean(string key, bool defaults)
        {
            return scope.GetBoolean(key, defaults);
        }

        public void PutString(string key, string value)
        {
            scope.Edit().PutString(key, value).Apply();
        }

        public string GetString(string key, string defaults = null)
        {
            return scope.GetString(key, defaults);
        }

        public void Remove(string key)
        {
            scope.Edit().Remove(key).Apply();
        }

        public void Clear()
        {
            scope.Edit().Clear().Apply();
        }
    }
}