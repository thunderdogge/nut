using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Nut.Ioc.Generator.Parsers
{
    public class NutIocSyntaxUsingsParser
    {
        public class Result
        {
            private string[] usings;
            public string[] Usings
            {
                get { return usings ?? (usings = new string[0]); }
                set { usings = value; }
            }
        }

        public Result ParseNamespaceInfo(SyntaxNode[] treeNodes)
        {
            var parseResult = new Result();
            var usingList = new List<string>();
            var usingNodes = treeNodes.OfType<UsingDirectiveSyntax>().ToArray();

            foreach (var usingNode in usingNodes)
            {
                usingList.Add(usingNode.ToString().Replace("using", null).Trim(';', ' '));
            }

            parseResult.Usings = usingList.ToArray();

            return parseResult;
        }
    }
}