using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nut.Ioc.Generator
{
    class Entry
    {
        private const string ConfigFileName = "nutioc.json";
        private const string TemplateDir = "Templates";
        private const string TemplateName = "NutIocTemplate.mustache";
        private const string ScannableFilesMask = "*.cs";

        static void Main(string[] args)
        {
            if (args == null || args.Length <= 3)
            {
                throw new ArgumentNullException(nameof(args), "Nut.Ioc.Generator input is empty");
            }

            var templatePath = GetTemplatePath();
            var templateRender = new NutIocTemplateRender();
            var generatedFilePath = new FileInfo(args[0]);
            var scannableDirPath = new DirectoryInfo(args[1]);
            var configFilePath = GetConfigPath(scannableDirPath);
            var assemblyName = args[2];
            var assemblyRootNamespace = args[3];
            var scannableFilePaths = GetScannableFilePaths(scannableDirPath);
            var generatedFileContent = templateRender.Render(templatePath, configFilePath, scannableFilePaths, assemblyRootNamespace);

            try
            {
                File.WriteAllText(generatedFilePath.FullName, generatedFileContent, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error while creating generated file: {e}");
            }
        }

        private static string GetConfigPath(DirectoryInfo root)
        {
            var configFilePath = root.GetFiles(ConfigFileName, SearchOption.AllDirectories).FirstOrDefault();
            return configFilePath?.FullName;
        }

        private static string[] GetScannableFilePaths(DirectoryInfo root)
        {
            var scannableFilePaths = root.GetFiles(ScannableFilesMask, SearchOption.AllDirectories);
            return scannableFilePaths.Where(x => !x.FullName.EndsWith(".Designer.cs") && !x.FullName.EndsWith(".Generated.cs")).Select(x => x.FullName).ToArray();
        }

        private static string GetTemplatePath()
        {
            var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (assemblyDir == null)
            {
                throw new ArgumentNullException(nameof(assemblyDir));
            }

            return Path.Combine(assemblyDir, TemplateDir, TemplateName);
        }
    }
}
