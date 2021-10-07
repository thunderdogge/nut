using Foundation;
using Nut.Core.Environment;

namespace Nut.iOS.Environment
{
    public abstract class NutIosPreferences : INutPreferences
    {
        private readonly NSUserDefaults scope;

        protected NutIosPreferences(NSUserDefaults scope)
        {
            this.scope = scope;
        }

        public bool Contains(string key)
        {
            return scope.ValueForKey((NSString)key) != null;
        }

        public void PutInt(string key, int value)
        {
            scope.SetInt(value, key);
            scope.Synchronize();
        }

        public int GetInt(string key, int defaults = 0)
        {
            return Contains(key) ? (int)scope.IntForKey(key) : defaults;
        }

        public void PutLong(string key, long value)
        {
            PutString(key, value.ToString());
        }

        public long GetLong(string key, long defaults = 0)
        {
            var value = GetString(key);

            long result;
            return value != null && long.TryParse(value, out result) ? result : defaults;
        }

        public void PutBoolean(string key, bool value)
        {
            scope.SetBool(value, key);
            scope.Synchronize();
        }

        public bool GetBoolean(string key, bool defaults)
        {
            return Contains(key) ? scope.BoolForKey(key) : defaults;
        }

        public void PutString(string key, string value)
        {
            scope.SetString(value ?? string.Empty, key);
            scope.Synchronize();
        }

        public string GetString(string key, string defaults = null)
        {
            return Contains(key) ? scope.StringForKey(key) : defaults;
        }

        public void Remove(string key)
        {
            scope.RemoveObject(key);
            scope.Synchronize();
        }

        public void Clear()
        {
            scope.RemovePersistentDomain(NSBundle.MainBundle.BundleIdentifier);
            scope.Synchronize();
        }
    }
}