using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace Nut.Core.Tests.Constraints
{
    public class ObjectComparer
    {
        private readonly HashSet<Type> immutableTypes;
        private readonly HashSet<Type> equatableTypes;

        public ObjectComparer(IEnumerable<Type> immutableTypes = null, IEnumerable<Type> equatableTypes = null)
        {
            this.immutableTypes = new HashSet<Type>(immutableTypes ?? Enumerable.Empty<Type>()) {typeof(string)};
            this.equatableTypes = new HashSet<Type>(equatableTypes ?? Enumerable.Empty<Type>()) {typeof(string), typeof(decimal), typeof(Guid), typeof(DateTime), typeof(TimeSpan) };
            CheckReferenceTypes(this.immutableTypes);
            CheckEquatableTypes(this.equatableTypes);
        }

        private static void CheckEquatableTypes(IEnumerable<Type> types)
        {
            var badTypes = types.Where(type => !type.GetInterfaces().Contains(typeof (IEquatable<>).MakeGenericType(type))).ToArray();
            if (badTypes.Any())
                throw new Exception($"Типы не реализуют интерфейс IEquatble<T>:\n{string.Join("\n", badTypes.Select(x => $"\t{x.Name}"))}");
        }

        private static void CheckReferenceTypes(IEnumerable<Type> types)
        {
            var badTypes = types.Where(type => !type.IsClass).ToArray();
            if (badTypes.Any())
                throw new Exception($"Типы не являются ссылочными:\n{string.Join("\n", badTypes.Select(x => $"\t{x.Name}"))}");
        }

        public static bool ObjectEquals(object expected, object actual)
        {
            string description;
            return new ObjectComparer().Equals(expected, actual, out description);
        }

        public bool Equals(object expected, object actual, out string errorDescription, bool compareNonPublicMembers = false)
        {
            return GetResult(expected, actual, true, compareNonPublicMembers, "", out errorDescription);
        }
        
        public bool EqualsButNotSame(object expected, object actual, out string errorDescription, bool сompareNonPublicMembers = false)
        {
            return GetResult(expected, actual, false, сompareNonPublicMembers, "", out errorDescription);
        }

        private bool GetResult(object expected, object actual, bool canObjectsBeTheSame, bool сompareNonPublicMembers, string path, out string errorDescription)
        {
            errorDescription = null;

            if (expected == null && actual == null)
                return true;

            if (expected == null || actual == null)
                return Fail(expected, actual, path, out errorDescription);

            if (expected.GetType() != actual.GetType())
                return FailByTypeMismatch(expected, actual, path, out errorDescription);

            var type = expected.GetType();

            if ((actual as Array)?.Length == 0 && (expected as Array)?.Length == 0)
                return true;

            if (canObjectsBeTheSame && ReferenceEquals(expected, actual))
                return true;

            if (!canObjectsBeTheSame && !immutableTypes.Contains(type) && ReferenceEquals(expected, actual))
                return FailByReferenceEquals(expected, actual, path, out errorDescription);

            if (type.IsPrimitive || type.IsEnum || equatableTypes.Contains(type))
                return expected.Equals(actual) || Fail(expected, actual, path, out errorDescription);

            if (actual is Array && expected is Array)
                return GetResult((Array) expected, (Array) actual, canObjectsBeTheSame, сompareNonPublicMembers, path, out errorDescription);

            if (actual is NameValueCollection && expected is NameValueCollection)
                return GetResult((NameValueCollection) expected, (NameValueCollection) actual, canObjectsBeTheSame, сompareNonPublicMembers, path, out errorDescription);

            if (actual is IEnumerable && expected is IEnumerable)
                return GetResult((IEnumerable) expected, (IEnumerable) actual, canObjectsBeTheSame, сompareNonPublicMembers, path, out errorDescription);

            return CompareMembers(expected, actual, canObjectsBeTheSame, сompareNonPublicMembers, path, out errorDescription);
        }

        private static bool Fail(object expected, object actual, string path, out string errorDescription)
        {
            return Fail(path, $"Expected: {expected ?? "null"}\nActual: {actual ?? "null"}", out errorDescription);
        }

        private static bool FailByTypeMismatch(object expected, object actual, string path, out string errorDescription)
        {
            return Fail(path, $"Expected type: {expected.GetType().Name}\nActual type: {actual.GetType().Name}", out errorDescription);
        }

        private static bool FailByReferenceEquals(object expected, object actual, string path, out string errorDescription)
        {
            return Fail(path, $"References on {expected} and {actual} are equal", out errorDescription);
        }

        private bool GetResult(Array expected, Array actual, bool canObjectsBeTheSame, bool сompareNonPublicMembers, string path, out string errorDescription)
        {
            if (expected.Length != actual.Length)
                return FailByArrayLength(expected.Length, actual.Length, path, out errorDescription);

            for (var i = 0; i < expected.Length; i++)
                if (!GetResult(expected.GetValue(i), actual.GetValue(i), canObjectsBeTheSame, сompareNonPublicMembers, CombinePath(path, $"[{i.ToString()}]", ""), out errorDescription))
                    return false;

            errorDescription = null;
            return true;
        }

        private static bool FailByArrayLength(int expected, int actual, string path, out string errorDescription)
        {
            return Fail(path, $"Array length differs.\nExpected: {expected.ToString()}\nActual: {actual.ToString()}", out errorDescription);
        }

        private bool GetResult(IEnumerable expected, IEnumerable actual, bool canObjectsBeTheSame, bool сompareNonPublicMembers, string path, out string errorDescription)
        {
            var expectedEnumerator = expected.GetEnumerator();
            var actualEnumerator = actual.GetEnumerator();
            var index = 0;

            while (true)
            {
                var actualHasNext = actualEnumerator.MoveNext();
                var expectedHasNext = expectedEnumerator.MoveNext();

                if (actualHasNext && !expectedHasNext)
                    return FailByEnumerableLength(index, path, out errorDescription);
                if (!actualHasNext && expectedHasNext)
                    return FailByEnumerableLength(index, path, out errorDescription);

                if (!actualHasNext || !expectedHasNext)
                    break;

                if (!GetResult(expectedEnumerator.Current, actualEnumerator.Current, canObjectsBeTheSame, сompareNonPublicMembers, CombinePath(path, $"[{index.ToString()}]", ""), out errorDescription))
                    return false;

                index++;
            }
            errorDescription = null;
            return true;
        }

        private static bool FailByEnumerableLength(int index, string path, out string errorDescription)
        {
            return Fail(path, $"Enumerable length differs.\nLast visited index: {index.ToString()}", out errorDescription);
        }

        private bool CompareMembers(object expected, object actual, bool canObjectsBeTheSame, bool сompareNonPublicMembers, string path, out string errorDescription)
        {
            string description = null;

            var bindingAttr = BindingFlags.Public | BindingFlags.Instance;
            if (сompareNonPublicMembers)
                bindingAttr |= BindingFlags.NonPublic;

            if (expected.GetType().GetProperties(bindingAttr)
                                  .Where(info => info.GetGetMethod() != null && !info.GetGetMethod().GetParameters().Any() /*Exclude indexer*/)
                                  .Any(p => !GetResult(GetPropertyValue(p, expected), GetPropertyValue(p, actual), canObjectsBeTheSame, сompareNonPublicMembers, CombinePath(path, p.Name), out description)))
            {
                errorDescription = description;
                return false;
            }

            if (expected.GetType().GetFields(bindingAttr)
                                  .Any(p => !GetResult(p.GetValue(expected), p.GetValue(actual), canObjectsBeTheSame, сompareNonPublicMembers, CombinePath(path, p.Name), out description)))
            {
                errorDescription = description;
                return false;
            }

            errorDescription = null;
            return true;
        }

        private static bool Fail(string path, string description, out string errorDescription)
        {
            errorDescription = $"Path: '{path}'\n{description}";
            return false;
        }

        private static string CombinePath(string path, string suffix, string separator = ".")
        {
            return string.IsNullOrWhiteSpace(path) ? suffix : $"{path}{separator}{suffix}";
        }

        private static object GetPropertyValue(PropertyInfo p, object expected)
        {
            return p.GetGetMethod().Invoke(expected, new object[0]);
        }
    }
}