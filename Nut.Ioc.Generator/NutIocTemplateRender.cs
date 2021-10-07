using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Nut.Ioc.Generator.Parsers;

namespace Nut.Ioc.Generator
{
    public class NutIocTemplateRender
    {
        private readonly NutIocSyntaxParser syntaxParser;
        private readonly NutIocConfigCreator configCreator;
        private readonly NutIocTemplateCreator templateCreator;

        public NutIocTemplateRender()
        {
            syntaxParser = new NutIocSyntaxParser();
            configCreator = new NutIocConfigCreator();
            templateCreator = new NutIocTemplateCreator();
        }

        public string Render(string templatePath, string configFilePath, string[] filePaths, string assemblyRootNamespace)
        {
            var config = configCreator.CreateConfig(configFilePath);
            var template = File.ReadAllText(templatePath, Encoding.UTF8);
            var syntaxTrees = CreateSyntaxTrees(config, filePaths);
            var parseResult = syntaxParser.ParseSyntax(syntaxTrees);
            var templateModel = templateCreator.CreateTemplate(assemblyRootNamespace, parseResult);

            return Nustache.Core.Render.StringToString(template, templateModel);
        }

        private static SyntaxTree[] CreateSyntaxTrees(NutIocConfig config, string[] filePaths)
        {
            var syntaxTrees = new List<SyntaxTree>();
            var normalizePattern = new Regex("[/\\\\]+", RegexOptions.IgnoreCase);
            var ignorePaths = config.IgnorePaths.Select(x => normalizePattern.Replace(x, Path.DirectorySeparatorChar.ToString())).ToArray();

            foreach (var filePath in filePaths)
            {
                if (ignorePaths.Any(x => filePath.Contains(x)))
                {
                    continue;
                }

                var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(filePath));
                syntaxTrees.Add(syntaxTree);
            }

            return syntaxTrees.ToArray();
        }
    }
}