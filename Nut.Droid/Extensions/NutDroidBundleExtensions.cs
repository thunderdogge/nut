using System;
using Android.Content;
using Android.OS;

namespace Nut.Droid.Extensions
{
    public static class NutDroidBundleExtensions
    {
        public static void PutGuid(this Bundle bundle, string key, Guid value)
        {
            bundle.PutString(key, value.ToString());
        }

        public static Guid? GetGuid(this Bundle bundle, string key)
        {
            Guid value;
            return Guid.TryParse(bundle.GetString(key, string.Empty), out value) ? value : (Guid?)null;
        }

        public static void PutEnum<TEnum>(this Bundle source, string key, TEnum value)
        {
            source.PutString(key, value.ToString());
        }

        public static void PutEnum<TEnum>(this Intent source, string key, TEnum value)
        {
            source.PutExtra(key, value.ToString());
        }

        public static TEnum GetEnum<TEnum>(this Intent source, string key, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            return source.Extras.GetEnum(key, defaultValue);
        }

        public static TEnum GetEnum<TEnum>(this Bundle source, string key, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            var valueString = source.GetString(key);
            TEnum valueParsed;
            return Enum.TryParse(valueString, out valueParsed) ? valueParsed : defaultValue;
        }
    }
}