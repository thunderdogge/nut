using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nut.Core.Platform;

namespace Nut.Core.Extensions
{
    public static class NutReflectionExtensions
    {
        public static IEnumerable<Type> GetTypes(this Assembly assembly)
        {
            foreach (var definedType in assembly.DefinedTypes)
            {
                yield return definedType.AsType();
            }
        }

        public static IEnumerable<Type> ExceptionSafeGetTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                return new Type[0];
            }
        }

        public static PropertyInfo GetNamedProperty(this Type type, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return null;
            }

            return GetProperties(type).FirstOrDefault(x => x.Name == propertyName);
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
            return GetProperties(type, BindingFlags.FlattenHierarchy | BindingFlags.Public);
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type, BindingFlags flags)
        {
            IEnumerable<PropertyInfo> properties;
            if ((flags & BindingFlags.FlattenHierarchy) == 0)
            {
                properties = type.GetTypeInfo().DeclaredProperties;
            }
            else
            {
                properties = type.GetRuntimeProperties();
            }

            foreach (var property in properties)
            {
                var getMethod = property.GetMethod;
                var setMethod = property.SetMethod;
                if (getMethod == null && setMethod == null)
                {
                    continue;
                }

                var publicTest = (flags & BindingFlags.Public) != BindingFlags.Public ||
                    getMethod.NullSafeIsPublic() || setMethod.NullSafeIsPublic();

                var instanceTest = (flags & BindingFlags.Instance) != BindingFlags.Instance ||
                    !getMethod.NullSafeIsStatic() || !setMethod.NullSafeIsStatic();

                var staticTest = (flags & BindingFlags.Static) != BindingFlags.Static ||
                    getMethod.NullSafeIsStatic() || setMethod.NullSafeIsStatic();

                if (publicTest && instanceTest && staticTest)
                {
                    yield return property;
                }
            }
        }

        public static MethodInfo GetNamedMethod(this Type type, string methodName)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                return null;
            }

            return GetMethods(type).FirstOrDefault(x => x.Name == methodName);
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type)
        {
            return GetMethods(type, BindingFlags.FlattenHierarchy | BindingFlags.Public);
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type, BindingFlags flags)
        {
            IEnumerable<MethodInfo> methods;
            if ((flags & BindingFlags.FlattenHierarchy) == 0)
            {
                methods = type.GetTypeInfo().DeclaredMethods;
            }
            else
            {
                methods = type.GetRuntimeMethods();
            }

            foreach (var method in methods)
            {
                var publicTest = (flags & BindingFlags.Public) != BindingFlags.Public || method.NullSafeIsPublic();
                var instanceTest = (flags & BindingFlags.Instance) != BindingFlags.Instance || !method.NullSafeIsStatic();
                var staticTest = (flags & BindingFlags.Static) != BindingFlags.Static || method.NullSafeIsStatic();

                if (publicTest && instanceTest && staticTest)
                {
                    yield return method;
                }
            }
        }

        private static bool NullSafeIsPublic(this MethodInfo info)
        {
            return info != null && info.IsPublic;
        }

        private static bool NullSafeIsStatic(this MethodInfo info)
        {
            return info != null && info.IsStatic;
        }
    }
}