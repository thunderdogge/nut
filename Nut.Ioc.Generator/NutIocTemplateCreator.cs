using System.Collections.Generic;
using System.Linq;
using Nut.Ioc.Generator.Extensions;
using Nut.Ioc.Generator.Parsers;
using Nut.Ioc.Generator.Templates;

namespace Nut.Ioc.Generator
{
    public class NutIocTemplateCreator
    {
        public NutIocTemplate CreateTemplate(string assemblyRootNamespace, NutIocSyntaxParseResult parseResult)
        {
            return new NutIocTemplate
            {
                Usings = parseResult.Usings,
                Namespace = assemblyRootNamespace,
                ClassName = NutIocConstants.GeneratedClassName,
                InterfaceName = NutIocConstants.GeneratedClassInterfaceName,
                ServiceDescriptions = CreateServiceDescriptions(parseResult)
            };
        }

        private static string[] CreateServiceDescriptions(NutIocSyntaxParseResult parseResult)
        {
            var serviceDescriptions = new List<string>();
            if (parseResult.Services != null)
            {
                var baseTypeCodes = new HashSet<string>();
                foreach (var service in parseResult.Services)
                {
                    var serviceDescription = CreateServiceDescription(service, parseResult);
                    foreach (var descriptionItem in serviceDescription)
                    {
                        if (baseTypeCodes.Add(descriptionItem.Key))
                        {
                            var serviceItemDescription = CreateServiceItemDescription(descriptionItem.Key, descriptionItem.Value);
                            serviceDescriptions.Add(serviceItemDescription);
                        }
                    }
                }
            }

            return serviceDescriptions.ToArray();
        }

        private static IEnumerable<KeyValuePair<string , string>> CreateServiceDescription(NutIocSyntaxParseResultService service, NutIocSyntaxParseResult parseResult)
        {
            if (service.Class.IsGeneric)
            {
                yield break;
            }

            foreach (var internalServiceDescription in CreateServiceDescriptionInternal(service, parseResult))
            {
                yield return internalServiceDescription;
            }
        }

        private static IEnumerable<KeyValuePair<string, string>> CreateServiceDescriptionInternal(NutIocSyntaxParseResultService service, NutIocSyntaxParseResult parseResult)
        {
            // Register class
            if (service.Class != null)
            {
                if (!service.Class.IsAbstract)
                {
                    var classTypeCode = CreateTypeResolveKey(service.Class);
                    var classResolveCode = CreateServiceResolveValue(service, service.Class);
                    yield return new KeyValuePair<string, string>(classTypeCode, classResolveCode);
                }
            }

            // Register class base type
            if (service.Base != null)
            {
                var baseTypeCode = CreateTypeResolveKey(service.Base);
                var baseResolveCode = CreateTypeResolveValue(service.Class);
                yield return new KeyValuePair<string, string>(baseTypeCode, baseResolveCode);
            }

            // Register class generic constructor parameters
            if (service.Class != null)
            {
                var genericCtorParameters = service.Class.ConstructorParameters.Where(x => x.IsGeneric);
                foreach (var ctorParameter in genericCtorParameters)
                {
                    // Find target generic service
                    var targetGenericService = parseResult.Services.FirstOrDefault(x =>
                    {
                        var name = ctorParameter.Name.StripGenericParameters();
                        return x.Base.Name.StripGenericParameters() == name || x.Class.Name.StripGenericParameters() == name;
                    });

                    // Enclose target generic service generic params
                    if (targetGenericService != null)
                    {
                        var patchedGenericService = targetGenericService.Clone();
                        patchedGenericService.Base?.EncloseGenericParameters(ctorParameter.GenericParameters);
                        patchedGenericService.Class?.EncloseGenericParameters(ctorParameter.GenericParameters);

                        // Add new generic service descriptions
                        foreach (var genericServiceDescription in CreateServiceDescriptionInternal(patchedGenericService, parseResult))
                        {
                            yield return genericServiceDescription;
                        }
                    }
                }
            }
        }

        private static string CreateServiceResolveValue(NutIocSyntaxParseResultService service, NutIocSyntaxParseResultType type)
        {
            if (type.IsAbstract)
            {
                return CreateTypeResolveValue(service.Base);
            }

            var parametersList = new List<string>();
            foreach (var constructorParameter in type.ConstructorParameters)
            {
                var parameterTypeCode = CreateTypeResolveKey(constructorParameter);
                parametersList.Add($"({constructorParameter.Name})x({parameterTypeCode})");
            }

            return $"x => new {type.Namespace.Append(".")}{type.Name}({string.Join(", ", parametersList)})";
        }

        private static string CreateTypeResolveKey(NutIocSyntaxParseResultType type)
        {
            return $"typeof({type.Namespace.Append(".")}{type.Name})";
        }

        private static string CreateTypeResolveValue(NutIocSyntaxParseResultType type)
        {
            return $"x => x({CreateTypeResolveKey(type)})";
        }

        private static string CreateServiceItemDescription(string baseTypeCode, string resolveTypeCode)
        {
            return $"new KeyValuePair<Type, Func<Func<Type, object>, object>>({baseTypeCode}, {resolveTypeCode})";
        }
    }
}