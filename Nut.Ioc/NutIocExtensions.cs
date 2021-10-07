using System;
using System.Collections.Generic;

namespace Nut.Ioc
{
    public static class NutIocExtensions
    {
        public static IEnumerable<KeyValuePair<Type, Func<Func<Type, object>, object>>> GetAssemblyServiceDescriptions(this Type assemblyType)
        {
            var typeNamespace = assemblyType.Namespace;
            var assemblyFullName = assemblyType.AssemblyQualifiedName.Replace(assemblyType.FullName, null).Trim(',', ' ');
            var serviceDescriptionsType = GetServiceDescriptionsType(typeNamespace, NutIocConstants.GeneratedClassName, assemblyFullName);

            if (serviceDescriptionsType == null)
            {
                throw new Exception("Cannot find generated dependencies");
            }

            return GetServiceDescriptionsInternal(serviceDescriptionsType);
        }

        private static Type GetServiceDescriptionsType(string startNamespace, string targetTypeName, string assemblyFullName)
        {
            Type targetType;
            var loopNamespace = startNamespace;

            while (true)
            {
                targetType = Type.GetType($"{loopNamespace}.{targetTypeName}, {assemblyFullName}");
                if (targetType != null)
                {
                    break;
                }

                var separatorIndex = loopNamespace.LastIndexOf('.');
                if (separatorIndex == -1)
                {
                    break;
                }

                loopNamespace = loopNamespace.Substring(0, separatorIndex);
            }

            return targetType;
        }

        private static IEnumerable<KeyValuePair<Type, Func<Func<Type, object>, object>>> GetServiceDescriptionsInternal(Type generatedType)
        {
            var dependencies = Activator.CreateInstance(generatedType) as IEnumerable<KeyValuePair<Type, Func<Func<Type, object>, object>>>;
            if (dependencies == null)
            {
                throw new Exception("Cannot create instance of " + generatedType.AssemblyQualifiedName);
            }

            return dependencies;
        }
    }
}