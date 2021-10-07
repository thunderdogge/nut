using System;
using Nut.Core.Extensions;

namespace Nut.Core.Environment
{
    public static class NutPreferencesExtentions
    {
        public static void PutGuid(this INutPreferences pref, string key, Guid value)
        {
            pref.PutString(key, value.ToString());
        }

        public static Guid GetGuid(this INutPreferences pref, string key)
        {
            return pref.GetString(key).ToGuidOrEmpty();
        }

        public static void PutEnum<T>(this INutPreferences pref, string key, T value) where T : struct
        {
            pref.PutString(key, value.ToString());
        }

        public static T GetEnum<T>(this INutPreferences pref, string key, T defaults = default(T)) where T : struct
        {
            T parsed;
            return Enum.TryParse(pref.GetString(key), true, out parsed) ? parsed : defaults;
        }

        public static void PutDateTime(this INutPreferences pref, string key, DateTime value, DateTimeKind kind)
        {
            var dateTime = DateTime.SpecifyKind(value, kind);
            pref.PutLong(key, dateTime.Ticks);
        }

        public static DateTime? GetDateTime(this INutPreferences pref, string key, DateTime? defaults, DateTimeKind kind)
        {
            if (pref.Contains(key))
            {
                return new DateTime(pref.GetLong(key), kind);
            }

            return defaults;
        }

        public static void PutDateTimeLocal(this INutPreferences pref, string key, DateTime value)
        {
            pref.PutDateTime(key, value, DateTimeKind.Local);
        }

        public static DateTime? GetDateTimeLocal(this INutPreferences pref, string key, DateTime? defaults = null)
        {
            return pref.GetDateTime(key, defaults, DateTimeKind.Local);
        }

        public static void PutDateTimeUtc(this INutPreferences pref, string key, DateTime value)
        {
            pref.PutDateTime(key, value, DateTimeKind.Utc);
        }

        public static DateTime? GetDateTimeUtc(this INutPreferences pref, string key, DateTime? defaults = null)
        {
            return pref.GetDateTime(key, defaults, DateTimeKind.Utc);
        }
    }
}