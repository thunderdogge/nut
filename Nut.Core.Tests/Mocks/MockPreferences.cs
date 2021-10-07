using System.Collections.Generic;
using Nut.Core.Environment;

namespace Nut.Core.Tests.Mocks
{
    public class MockPreferences : INutPreferences
    {
        private readonly Dictionary<string, object> scope;

        public MockPreferences()
        {
            scope = new Dictionary<string, object>();
        }

        public bool Contains(string key)
        {
            return scope.ContainsKey(key);
        }

        public void PutInt(string key, int value)
        {
            scope[key] = value;
        }

        public int GetInt(string key, int defaults = 0)
        {
            if (scope.ContainsKey(key))
            {
                int parsed;
                return int.TryParse(scope[key].ToString(), out parsed) ? parsed : defaults;
            }

            return defaults;
        }

        public void PutLong(string key, long value)
        {
            scope[key] = value;
        }

        public long GetLong(string key, long defaults = 0)
        {
            if (scope.ContainsKey(key))
            {
                long parsed;
                return long.TryParse(scope[key].ToString(), out parsed) ? parsed : defaults;
            }

            return defaults;
        }

        public void PutBoolean(string key, bool value)
        {
            scope[key] = value;
        }

        public bool GetBoolean(string key, bool defaults)
        {
            if (scope.ContainsKey(key))
            {
                bool parsed;
                return bool.TryParse(scope[key].ToString(), out parsed) ? parsed : defaults;
            }

            return defaults;
        }

        public void PutString(string key, string value)
        {
            scope[key] = value;
        }

        public string GetString(string key, string defaults = null)
        {
            if (scope.ContainsKey(key))
            {
                return scope[key].ToString();
            }

            return defaults;
        }

        public void Remove(string key)
        {
            scope.Remove(key);
        }

        public void Clear()
        {
            scope.Clear();
        }
    }
}