using System;
using System.Text.RegularExpressions;

namespace Nut.Core.Extensions
{
    public static class NutStringExtensions
    {
        public const string Dash = "—";
        public const string Ellipsis = "...";
        public static Regex UrlPattern = new Regex("^(https?://|ftp://|www\\.|[^\\s:=]+@www\\.)[^\\s/$.?#].[^\\s]*$", RegexOptions.IgnoreCase);
        public static Regex EmailPattern = new Regex("^[a-zA-Z0-9!#$%&\'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&\'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", RegexOptions.IgnoreCase);
        public static Regex PhonePattern = new Regex("^(?:\\+\\d{1,3}|0\\d{1,3}|00\\d{1,2})?(?:\\s?\\(\\d+\\))?(?:[-\\/\\s.]|\\d)+$", RegexOptions.IgnoreCase);

        public static string Or(this string source, string target)
        {
            return string.IsNullOrWhiteSpace(source) ? target : source;
        }

        public static string OrDash(this string source)
        {
            return string.IsNullOrWhiteSpace(source) ? Dash : source;
        }

        public static string Ellipsize(this string source, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return string.Empty;
            }

            return source.Length <= maxLength ? source : $"{source.Substring(0, maxLength)}{Ellipsis}";
        }

        public static string Simplify(this string source)
        {
            return Regex.Replace(source ?? string.Empty, @"\s+", " ").Trim();
        }

        public static Guid? ToGuid(this string source, Guid? defaultValue = null)
        {
            Guid id;
            return Guid.TryParse(source, out id) ? id : defaultValue;
        }

        public static Guid ToGuidOrEmpty(this string source)
        {
            return ToGuid(source) ?? Guid.Empty;
        }

        public static TEnum ToEnum<TEnum>(this string source) where TEnum : struct
        {
            TEnum value;
            return Enum.TryParse(source, true, out value) ? value : default(TEnum);
        }

        public static bool IsUrlFormat(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            return UrlPattern.IsMatch(source);
        }

        public static bool IsEmailFormat(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            return EmailPattern.IsMatch(source);
        }

        public static bool IsPhoneFormat(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            return PhonePattern.IsMatch(source);
        }
    }
}