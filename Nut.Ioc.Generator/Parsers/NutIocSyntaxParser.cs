using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nut.Ioc.Generator.Extensions;

namespace Nut.Ioc.Generator.Parsers
{
    public class NutIocSyntaxParser
    {
        private readonly NutIocSyntaxClassParser syntaxClassParser;
        private readonly NutIocSyntaxUsingsParser syntaxUsingsParser;
        private readonly NutIocSyntaxBaseTypeParser syntaxBaseTypeParser;
        private readonly NutIocSyntaxNamespaceParser syntaxNamespaceParser;
        private readonly NutIocSyntaxConstructorParser syntaxConstructorParser;

        public NutIocSyntaxParser()
        {
            syntaxClassParser = new NutIocSyntaxClassParser();
            syntaxUsingsParser = new NutIocSyntaxUsingsParser();
            syntaxBaseTypeParser = new NutIocSyntaxBaseTypeParser();
            syntaxNamespaceParser = new NutIocSyntaxNamespaceParser();
            syntaxConstructorParser = new NutIocSyntaxConstructorParser();
        }

        public NutIocSyntaxParseResult ParseSyntax(params SyntaxTree[] syntaxTrees)
        {
            var parseResult = new NutIocSyntaxParseResult();
            var parseResultUsings = new HashSet<string>();
            var parseResultServices = new HashSet<NutIocSyntaxParseResultService>();

            foreach (var syntaxTree in syntaxTrees)
            {
                var treeRoot = (CompilationUnitSyntax)syntaxTree.GetRoot();
                var treeNodes = treeRoot.DescendantNodes().ToArray();
                var templateService = new NutIocSyntaxParseResultService();

                // Class info
                var classInfo = syntaxClassParser.ParseClassInfo(treeNodes);
                if (classInfo == null)
                {
                    continue;
                }

                // Namespace info
                var namespaceInfo = syntaxNamespaceParser.ParseNamespaceInfo(treeNodes);
                if (namespaceInfo == null)
                {
                    continue;
                }

                // Contructor info
                var constructorInfo = syntaxConstructorParser.ParseContructorInfo(treeNodes);
                if (constructorInfo == null)
                {
                    continue;
                }

                // Usings info
                var usingsInfo = syntaxUsingsParser.ParseNamespaceInfo(treeNodes);
                foreach (var usingString in usingsInfo.Usings)
                {
                    parseResultUsings.Add(usingString);
                }

                // Base type info
                var baseTypeInfo = syntaxBaseTypeParser.ParseBaseTypeInfo(treeNodes);

                parseResultUsings.Add(namespaceInfo.Name);

                if (!string.IsNullOrEmpty(classInfo.Name))
                {
                    templateService.Class = new NutIocSyntaxParseResultType
                    {
                        Name = classInfo.Name,
                        Namespace = namespaceInfo.Name,
                        GenericParameters = classInfo.Name.GetGenericParameters(),
                        IsAbstract = classInfo.IsAbstract
                    };

                    if (constructorInfo.Parameters.Length > 0)
                    {
                        templateService.Class.ConstructorParameters = constructorInfo.Parameters.SelectArray(x => new NutIocSyntaxParseResultType
                        {
                            Name = x,
                            GenericParameters = x.GetGenericParameters()
                        });
                    }
                }

                if (!string.IsNullOrEmpty(baseTypeInfo.Name))
                {
                    templateService.Base = new NutIocSyntaxParseResultType
                    {
                        Name = baseTypeInfo.Name,
                        GenericParameters = baseTypeInfo.Name.GetGenericParameters()
                    };
                }

                if (templateService.Class != null || templateService.Base != null)
                {
                    parseResultServices.Add(templateService);
                }
            }

            parseResult.Usings = parseResultUsings.ToArray();
            parseResult.Services = parseResultServices.ToArray();

            return parseResult;
        }
    }
}