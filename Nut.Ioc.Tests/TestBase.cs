using System.Linq;
using Nut.Ioc.Generator.Parsers;
using NUnit.Framework;

namespace Nut.Ioc.Tests
{
    public abstract class TestBase
    {
        [SetUp]
        public virtual void Setup()
        {
        }

        protected string BuildUsingsString(NutIocSyntaxParseResult template)
        {
            return "Usings: " + string.Join(", ", template.Usings);
        }

        public string BuildServicesString(NutIocSyntaxParseResult template)
        {
            return "Services: " + string.Join(", ", template.Services.Select(x => x.Class.Name));
        }
    }
}