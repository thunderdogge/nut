using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nut.Ioc.Generator.Extensions;

namespace Nut.Ioc.Generator.Parsers
{
    public class NutIocSyntaxConstructorParser
    {
        private static readonly HashSet<string> increatableConstructorParams = new HashSet<string>
        {
            "int",
            "uint",
            "double",
            "decimal",
            "byte",
            "long",
            "ulong",
            "float",
            "object",
            "string",
            "bool",
            "Guid",
            "Func",
            "Action",
            "IntPtr"
        };
        private static readonly HashSet<string> increatableConstructorParamsStart = new HashSet<string>
        {
            "Func<",
            "Action<",
            "ISet<",
            "HashSet<",
            "List<",
            "IList<",
            "Dictionary<",
            "IDictionary<",
            "IEnumerable<",
        };
        private static readonly HashSet<string> increatableConstructorParamsEnd = new HashSet<string>
        {
            "[]"
        };

        public class Result
        {
            private string[] parameters;
            public string[] Parameters
            {
                get { return parameters ?? (parameters = new string[0]); }
                set { parameters = value; }
            }
        }

        public Result ParseContructorInfo(SyntaxNode[] treeNodes)
        {
            var parseResult = new Result();
            var constructors = treeNodes.OfType<ConstructorDeclarationSyntax>().ToArray();
            var publicConstructor = ChooseSuitableConstructor(constructors);
            var classSyntax = treeNodes.OfType<ClassDeclarationSyntax>().FirstOrDefault();
            var classBaseTypes = classSyntax?.BaseList;

            // No public constructor
            if (publicConstructor == null)
            {
                return classBaseTypes == null ? null : parseResult;
            }

            var constructorParams = publicConstructor.ParameterList.Parameters;
            var constructorParamList = new List<string>();
            foreach (var constructorParam in constructorParams)
            {
                var paramTypeName = constructorParam.Type.ToString().Trim();

                // Empty parameter
                if (string.IsNullOrWhiteSpace(paramTypeName))
                {
                    continue;
                }

                // Cannot create class with some paramaters
                if (increatableConstructorParams.Contains(paramTypeName))
                {
                    return null;
                }
                if (increatableConstructorParamsEnd.Any(x => paramTypeName.EndsWith(x)))
                {
                    return null;
                }
                if (increatableConstructorParamsStart.Any(x => paramTypeName.StartsWith(x)))
                {
                    return null;
                }

                constructorParamList.Add(paramTypeName);
            }

            // Public constructor with empty argument list
            if (constructorParamList.Count == 0)
            {
                return classBaseTypes == null ? null : parseResult;
            }

            parseResult.Parameters = constructorParamList.ToArray();

            return parseResult;
        }

        private static ConstructorDeclarationSyntax ChooseSuitableConstructor(ConstructorDeclarationSyntax[] constructors)
        {
            var publicCtors = constructors.Where(x => x.Modifiers.Any(m => m.Text == "public")).ToArray();
            if (publicCtors.Length == 0)
            {
                return null;
            }

            if (publicCtors.Length == 1)
            {
                return publicCtors[0];
            }

            foreach (var publicCtor in publicCtors)
            {
                if (publicCtor.ParameterList.Parameters.Count == 0)
                {
                    return publicCtor;
                }

                var attributes = publicCtor.AttributeLists.SelectMany(x => x.Attributes.Select(a => a.Name.ToString())).ToHashSet();
                if (attributes.Contains("NutIocConstructor"))
                {
                    return publicCtor;
                }
            }

            return null;
        }
    }
}