using System.IO;
using Newtonsoft.Json;

namespace Nut.Ioc.Generator
{
    public class NutIocConfigCreator
    {
        public NutIocConfig CreateConfig(string filePath)
        {
            try
            {
                var fileContent = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<NutIocConfig>(fileContent);
            }
            catch
            {
                return new NutIocConfig();
            }
        }
    }
}