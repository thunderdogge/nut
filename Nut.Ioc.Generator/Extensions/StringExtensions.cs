using System;
using System.Linq;

namespace Nut.Ioc.Generator.Extensions
{
    public static class StringExtensions
    {
        public static string Append(this string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            return source + target;
        }

        public static string Prepend(this string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            return target + source;
        }

        public static bool IsGenericTypeName(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            return source.Contains("<") && source.EndsWith(">");
        }

        public static bool IsClassName(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            return !IsInterfaceName(source);
        }

        public static bool IsInterfaceName(this string source)
        {
            if (string.IsNullOrWhiteSpace(source) || source.Length < 2)
            {
                return false;
            }

            var firstChar = source[0];
            var secondChar = source[1].ToString();

            return firstChar == 'I' && secondChar != secondChar.ToLower();
        }

        public static string[] GetGenericParameters(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return new string[0];
            }

            var bracket1Index = source.IndexOf('<');
            if (bracket1Index < 0)
            {
                return new string[0];
            }

            var bracket2Index = source.IndexOf('>');
            if (bracket2Index < 0)
            {
                return new string[0];
            }

            var genericString = source.Substring(bracket1Index + 1, bracket2Index - bracket1Index - 1);
            return genericString.Split(new [] {","}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
        }

        public static string StripGenericParameters(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return null;
            }

            var bracketIndex = source.IndexOf('<');
            if (bracketIndex >= 0)
            {
                return source.Substring(0, bracketIndex);
            }

            return source;
        }

        public static string SwitchGenericParameters(this string source, string[] parameters)
        {
            if (!source.IsGenericTypeName())
            {
                return source;
            }

            var pureName = source.StripGenericParameters();
            if (parameters == null || parameters.Length == 0)
            {
                return pureName;
            }

            return $"{pureName}<{string.Join(", ", parameters)}>";
        }
    }
}