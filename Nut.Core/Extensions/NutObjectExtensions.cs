using System;

namespace Nut.Core.Extensions
{
    public static class NutObjectExtensions
    {
        public static bool IsTrueValue(object value)
        {
            if (value == null)
                return false;
            if (value is bool)
                return (bool)value;
            if (value is int)
                return (int)value > 0;
            if (value is double)
                return (double)value > 0.0;
            if (value is long)
                return (long)value > 0;
            if (value is Guid)
                return (Guid)value != Guid.Empty;
            if (value is string)
                return !string.IsNullOrWhiteSpace((string)value);
            return true;
        }
    }
}