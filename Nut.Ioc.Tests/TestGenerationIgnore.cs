using System;
using System.Collections;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Nut.Ioc.Generator.Parsers;
using NUnit.Framework;

namespace Nut.Ioc.Tests
{
    public class TestGenerationIgnore : TestBase
    {
        private NutIocSyntaxParser syntaxParser;

        public override void Setup()
        {
            syntaxParser = new NutIocSyntaxParser();
        }

        [Test]
        public void TestIgnoreEmptyNamespace()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                public class ResolvableClass
                {
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);

            Assert.IsEmpty(iocTemplate.Usings);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [Test]
        public void TestIgnoreEmptyClass()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                namespace SomeNamespace
                {
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);

            Assert.IsEmpty(iocTemplate.Usings);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [Test]
        public void TestIgnoreStaticClass()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                namespace SomeNamespace
                {
                    public static class SomeClass
                    {
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);

            Assert.IsEmpty(iocTemplate.Usings);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [Test]
        public void TestIgnorePrivateClassWithModifier()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                namespace SomeNamespace
                {
                    private class SomeClass
                    {
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);

            Assert.IsEmpty(iocTemplate.Usings);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [Test]
        public void TestIgnorePrivateClassWithoutModifier()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                namespace SomeNamespace
                {
                    class SomeClass
                    {
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);

            Assert.IsEmpty(iocTemplate.Usings);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [Test]
        public void TestIgnoreSimpleClassWithoutDependencies()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;

                namespace SomeNamespace
                {
                    public class SomeClass
                    {
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);

            Assert.IsEmpty(iocTemplate.Usings);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [Test]
        public void TestIgnoreMultiplePublicNotEmptyConstructors()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;

                namespace SomeNamespace
                {
                    public class SomeClass
                    {
                        public SomeClass(ISomeService service)
                        {
                        }

                        public SomeClass(ISomeServiceFactory factory)
                        {
                        }
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);

            Assert.IsEmpty(iocTemplate.Usings);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [TestCase("bool")]
        [TestCase("Guid")]
        [TestCase("string")]
        [TestCase("object")]
        [TestCase("byte")]
        [TestCase("float")]
        [TestCase("double")]
        [TestCase("decimal")]
        [TestCase("int")]
        [TestCase("uint")]
        [TestCase("long")]
        [TestCase("ulong")]
        [TestCase("Func")]
        [TestCase("Action")]
        [TestCase("Func<int>")]
        [TestCase("Action<int>")]
        [TestCase("List<int>")]
        [TestCase("IList<int>")]
        [TestCase("HashSet<int>")]
        [TestCase("Dictionary<int>")]
        [TestCase("IEnumerable<int>")]
        public void TestIgnoreConstructorWithDefaultTypes(string type)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;

                namespace SomeNamespace
                {
                    public class SomeClass
                    {
                        public SomeClass(" + type + @" entity)
                        {
                        }
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);

            Assert.IsEmpty(iocTemplate.Usings);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [Test]
        public void TestIgnoreNotPublicConstructors()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;

                namespace SomeNamespace
                {
                    public class SomeClass
                    {
                        private SomeClass(ISomeService service)
                        {
                        }

                        protected SomeClass(ISomeService service, ISomeAnotherService anotherService)
                        {
                        }

                        internal SomeClass(ISomeServiceFactory factory)
                        {
                        }
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);

            Assert.IsEmpty(iocTemplate.Usings);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [Test]
        public void TestIgnoreDefaultSystemInterface()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System.IDisposable;

                namespace SomeNamespace
                {
                    public class SomeClass : IDisposable
                    {
                        public SomeClass(ISomeService service)
                        {
                        }
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            var targetService = iocTemplate.Services.SingleOrDefault(x => x.Class.Name == "SomeClass");

            Assert.NotNull(targetService);
            Assert.IsNull(targetService.Base);
        }

        [TestCase("Exception")]
        [TestCase("Attribute")]
        public void TestIgnoreSpecialBaseTypes(string baseClass)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText($@"
                using System;

                namespace SomeNamespace
                {{
                    public class SomeClass : {baseClass}
                    {{
                    }}
                }}
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [TestCase("[Activity]")]
        [TestCase("[ActivityAttribute]")]
        [TestCase("[Activity]\n[AnotherAttribute]")]
        [TestCase("[ActivityAttribute]\n[AnotherAttribute]")]
        [TestCase("[Activity(Theme = \"@style/SomeTheme\")]")]
        [TestCase("[ActivityAttribute(Theme = \"@style/SomeTheme\")]\n[AnotherAttribute]")]
        [TestCase("[NutIocIgnore]")]
        [TestCase("[NutIocIgnoreAttribute]")]
        [TestCase("[Application]")]
        [TestCase("[ApplicationAttribute]")]
        [TestCase("[AttributeUsage]")]
        [TestCase("[AttributeUsageAttribute]")]
        [TestCase("[DataContract]")]
        [TestCase("[DataContractAttribute]")]
        [TestCase("[Register]")]
        [TestCase("[RegisterAttribute]")]
        [TestCase("[Serializable]")]
        [TestCase("[SerializableAttribute]")]
        public void TestIgnoreSpecialAttributes(string attributes)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText($@"
                namespace SomeNamespace
                {{
                    {attributes}
                    public class SomeActivity
                    {{
                    }}
                }}
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            Assert.IsEmpty(iocTemplate.Services);
        }

        [TestCase(nameof(IComparer))]
        [TestCase(nameof(ICloneable))]
        [TestCase(nameof(IDisposable))]
        [TestCase(nameof(IComparable))]
        [TestCase(nameof(IEnumerable))]
        [TestCase(nameof(IEnumerator))]
        [TestCase("ICloneable<SomeClass>")]
        [TestCase("IComparable<SomeClass>")]
        [TestCase("IEquatable<SomeClass>")]
        [TestCase("IEnumerable<SomeClass>")]
        [TestCase("IEnumerator<SomeClass>")]
        public void TestIgnoreSpecialInterfaces(string interfaceName)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText($@"
                using System;

                namespace SomeNamespace
                {{
                    public class SomeClass : {interfaceName}
                    {{
                        public SomeClass(ISomeService service)
                        {{
                        }}
                    }}
                }}
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            var targetService = iocTemplate.Services.FirstOrDefault();

            Assert.NotNull(targetService);
            Assert.IsNull(targetService.Base);
        }

        [Test]
        public void TestIgnoreAbstractClassWithoutBaseTypes()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;

                namespace SomeNamespace
                {
                    public abstract class SomeBaseClass
                    {
                    }
                }
            ");

            var iocTemplate = syntaxParser.ParseSyntax(syntaxTree);
            var targetService = iocTemplate.Services.FirstOrDefault();

            Assert.IsNull(targetService);
        }
    }
}