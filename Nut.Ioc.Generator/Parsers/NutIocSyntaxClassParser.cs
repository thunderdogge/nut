using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nut.Ioc.Generator.Extensions;

namespace Nut.Ioc.Generator.Parsers
{
    public class NutIocSyntaxClassParser
    {
        private static readonly HashSet<string> increatableClassAttributes = new []
        {
            "NutIocIgnore",
            "Activity",
            "Application",
            "AttributeUsage",
            "DataContract",
            "Register",
            "Serializable"
        }.SelectMany(x => new[] {x, $"{x}Attribute"}).ToHashSet();

        private static readonly HashSet<string> increatableBaseClasses = new HashSet<string>
        {
            "Attribute",
            "Exception"
        };

        public class Result
        {
            public string Name { get; set; }

            public bool IsAbstract { get; set; }
        }

        public Result ParseClassInfo(SyntaxNode[] treeNodes)
        {
            var parseResult = new Result();
            var classSytaxes = treeNodes.OfType<ClassDeclarationSyntax>().ToArray();

            // Not single class declaration
            if (classSytaxes.Length != 1)
            {
                return null;
            }

            var classSytax = classSytaxes[0];

            // Class is not public
            if (classSytax.Modifiers.Count(x => x.Text == "public") == 0)
            {
                return null;
            }

            // Class is static
            if (classSytax.Modifiers.Any(x => x.Text == "static"))
            {
                return null;
            }

            // Class is abstract without base types
            if (classSytax.Modifiers.Any(x => x.Text == "abstract"))
            {
                if (classSytax.BaseList == null)
                {
                    return null;
                }

                parseResult.IsAbstract = true;
            }

            // Empty class name
            var className = classSytax.Identifier.Text;
            if (string.IsNullOrWhiteSpace(className))
            {
                return null;
            }

            // Class has special attributes
            var classAttributes = classSytax.AttributeLists.SelectMany(x => x.Attributes.Select(a => a.Name.ToString())).ToArray();
            if (classAttributes.Any(x => increatableClassAttributes.Contains(x)))
            {
                return null;
            }

            // Class extends special classes
            var classBaseList = classSytax.BaseList;
            if (classBaseList != null)
            {
                var classBaseNames = classBaseList.Types.SelectArray(x => x.Type.ToString().Trim());
                if (classBaseNames.Any(x => increatableBaseClasses.Contains(x)))
                {
                    return null;
                }
            }

            // Has generic
            var genericList = new List<string>();
            var genericParameters = classSytax.TypeParameterList?.Parameters;
            if (genericParameters?.Count > 0)
            {
                foreach (var genericParameter in genericParameters)
                {
                    genericList.Add(genericParameter.Identifier.ToString().Trim());
                }
            }

            parseResult.Name = className + (genericList.Count == 0 ? "" : "<" + string.Join(", ", genericList) + ">");

            return parseResult;
        }
    }
}