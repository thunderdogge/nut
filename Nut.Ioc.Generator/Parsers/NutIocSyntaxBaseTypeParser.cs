using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nut.Ioc.Generator.Extensions;

namespace Nut.Ioc.Generator.Parsers
{
    public class NutIocSyntaxBaseTypeParser
    {
        private static readonly HashSet<string> increatableNames = new HashSet<string>
        {
            nameof(IComparer),
            nameof(ICloneable),
            nameof(IDisposable),
            nameof(IComparable),
            nameof(IEnumerable),
            nameof(IEnumerator),
            nameof(INotifyPropertyChanged),
        };

        private static readonly HashSet<string> increatableNamesStart = new HashSet<string>
        {
            "ICloneable<",
            "IComparable<",
            "IEquatable<",
            "IEnumerable<",
            "IEnumerator<"
        };

        public class Result
        {
            public string Name { get; set; }
        }

        public Result ParseBaseTypeInfo(SyntaxNode[] treeNodes)
        {
            var parseResult = new Result();
            var classSyntax = treeNodes.OfType<ClassDeclarationSyntax>().FirstOrDefault();
            if (classSyntax == null)
            {
                return parseResult;
            }

            var classBaseList = classSyntax.BaseList;
            if (classBaseList == null)
            {
                return parseResult;
            }

            var baseTypes = classBaseList.Types.SelectArray(x => x.Type.ToString().Trim());

            var interfaceNames = baseTypes.Where(x => x.IsInterfaceName()).ToArray();
            var filteredInterfaceNames = interfaceNames.Where(x => !increatableNames.Contains(x) && !increatableNamesStart.Any(x.StartsWith)).ToArray();
            if (filteredInterfaceNames.Length > 0)
            {
                parseResult.Name = filteredInterfaceNames.FirstOrDefault();
                return parseResult;
            }

            parseResult.Name = baseTypes.FirstOrDefault(x => x.IsClassName());
            return parseResult;
        }
    }
}