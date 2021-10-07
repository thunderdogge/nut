using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Nut.Ioc.Generator.Parsers
{
    public class NutIocSyntaxNamespaceParser
    {
        public class Result
        {
            public string Name { get; set; }
        }

        public Result ParseNamespaceInfo(SyntaxNode[] treeNodes)
        {
            var parseResult = new Result();
            var namespaceNode = treeNodes.OfType<NamespaceDeclarationSyntax>().FirstOrDefault();

            if (namespaceNode == null)
            {
                return null;
            }

            var namespaceName = namespaceNode.Name.ToString().Trim();
            if (string.IsNullOrEmpty(namespaceName))
            {
                return null;
            }

            parseResult.Name = namespaceName;

            return parseResult;
        }
    }
}