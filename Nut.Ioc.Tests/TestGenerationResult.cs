using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Nut.Ioc.Generator.Parsers;
using NUnit.Framework;

namespace Nut.Ioc.Tests
{
    public class TestGenerationResult : TestBase
    {
        private NutIocSyntaxParser syntaxParser;

        public override void Setup()
        {
            syntaxParser = new NutIocSyntaxParser();
        }

        [Test]
        public void TestTemplateNamespace()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                namespace SomeNamespace
                {
                    public class SomeClass
                    {
                        public SomeClass(ISomeService service)
                        {
                        }
                    }
                }
            ");

            var parseResult = syntaxParser.ParseSyntax(syntaxTree);
            var targetService = parseResult.Services.SingleOrDefault();

            Assert.NotNull(targetService);
            Assert.AreEqual("SomeNamespace", targetService.Class.Namespace);
        }

        [TestCase("SomeClass", null, "SomeClass", null)]
        [TestCase("SomeClass", "", "SomeClass", null)]
        [TestCase("SomeClass", "SomeBaseClass", "SomeClass", "SomeBaseClass")]
        [TestCase("SomeClass", "ISomeInterface", "SomeClass", "ISomeInterface")]
        public void TestServiceParameters(string className, string interfaceName, string expectedClassName, string expectedInterfaceName)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText($@"
                namespace SomeNamespace
                {{
                    public class {className}{(string
                    .IsNullOrEmpty(interfaceName)
                    ? ""
                    : ": " + interfaceName)}
                    {{
                        public {className}(ISomeService service)
                        {{
                        }}
                    }}
                }}
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            var targetService = iocTemplate.Services.SingleOrDefault();

            Assert.NotNull(targetService);
            Assert.AreEqual(expectedClassName, targetService.Class?.Name);
            Assert.AreEqual(expectedInterfaceName, targetService.Base?.Name);
        }

        [Test]
        public void TestServiceUsings()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;
                using System;
                using System.Action;
                using System.Action;

                namespace SomeNamespace
                {
                    public class SomeClass
                    {
                        public SomeClass(ISomeService service)
                        {
                        }
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            var expectedUsings = new HashSet<string>
            {
                "System",
                "System.Action",
                "SomeNamespace",
            };
            Assert.AreEqual(expectedUsings, iocTemplate.Usings);
        }

        [Test]
        public void TestGeneric()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                namespace SomeNamespace
                {
                    public class SomeClass<TParam> : SomeBaseClass<TParam>, ISomeClass<TParam>
                    {
                        public SomeClass(ISomeService service)
                        {
                        }
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            var targetService = iocTemplate.Services.SingleOrDefault();

            Assert.NotNull(targetService);
            Assert.AreEqual("SomeClass<TParam>", targetService.Class.Name);
            Assert.AreEqual("ISomeClass<TParam>", targetService.Base.Name);
        }

        [Test]
        public void TestMultiGeneric()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                namespace SomeNamespace
                {
                    public class SomeClass<TParam1, TParam2, TParam3>
                        : SomeBaseClass<TParam1, TParam2, TParam3>, ISomeClass<TParam1, TParam2, TParam3>
                    {
                        public SomeClass(ISomeService service)
                        {
                        }
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            var targetService = iocTemplate.Services.SingleOrDefault();

            Assert.NotNull(targetService);
            Assert.AreEqual("SomeClass<TParam1, TParam2, TParam3>", targetService.Class.Name);
            Assert.AreEqual("ISomeClass<TParam1, TParam2, TParam3>", targetService.Base.Name);
        }

        [Test]
        public void TestMultipleConstructorsWithEmpty()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                namespace SomeNamespace
                {
                    public class SomeClass
                    {
                        public SomeClass()
                        {
                        }

                        public SomeClass(ISomeService service)
                        {
                        }
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            var targetService = iocTemplate.Services.SingleOrDefault();

            Assert.IsNull(targetService);
        }

        [Test]
        public void TestMultipleConstructorsWithAttribute()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                namespace SomeNamespace
                {
                    public class SomeClass
                    {
                        [NutIocConstructor]
                        public SomeClass(ISomeService1 service1)
                        {
                        }

                        public SomeClass(ISomeService2 service2)
                        {
                        }
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            var targetService = iocTemplate.Services.SingleOrDefault();

            Assert.NotNull(targetService);
            Assert.AreEqual("ISomeService1", targetService.Class.ConstructorParameters.FirstOrDefault()?.Name);
        }
    }
}