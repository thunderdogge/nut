using System;
using Nut.Core.Extensions;
using Nut.Core.Tests.Base;
using NUnit.Framework;

namespace Nut.Core.Tests.TestExtensions
{
    public class TestStringExtensions : TestBase
    {
        public enum TestStringEnum
        {
            Default,
            One,
            Two
        }

        [TestCase(null, "a", "a")]
        [TestCase("", "a", "a")]
        [TestCase(" ", "a", "a")]
        [TestCase("b", "a", "b")]
        public void TestOr(string source, string target, string expected)
        {
            Assert.AreEqual(expected, NutStringExtensions.Or(source, target));
        }

        [TestCase(null, "—")]
        [TestCase("", "—")]
        [TestCase(" ", "—")]
        [TestCase("a", "a")]
        public void TestOrDash(string source, string expected)
        {
            Assert.AreEqual(expected, NutStringExtensions.OrDash(source));
        }

        [TestCase(null, 1, "")]
        [TestCase("", 1, "")]
        [TestCase(" ", 0, "")]
        [TestCase(" ", 1, "")]
        [TestCase("a", 1, "a")]
        [TestCase("aa", 1, "a...")]
        [TestCase("abcde", 4, "abcd...")]
        public void TestEllipsize(string source, int maxLength, string expected)
        {
            Assert.AreEqual(expected, NutStringExtensions.Ellipsize(source, maxLength));
        }

        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase(" ", "")]
        [TestCase(" a ", "a")]
        [TestCase(" a b ", "a b")]
        [TestCase(" a  b ", "a b")]
        public void TestSimplify(string source, string expected)
        {
            Assert.AreEqual(expected, NutStringExtensions.Simplify(source));
        }

        [Test]
        public void TestToGuid()
        {
            string source = null;
            Guid? expected = null;
            Guid? defaults = null;
            Assert.AreEqual(expected, NutStringExtensions.ToGuid(source, defaults));

            source = "";
            Assert.AreEqual(expected, NutStringExtensions.ToGuid(source, defaults));

            source = " ";
            Assert.AreEqual(expected, NutStringExtensions.ToGuid(source, defaults));

            source = " a ";
            Assert.AreEqual(expected, NutStringExtensions.ToGuid(source, defaults));

            source = " a ";
            defaults = Guid.Empty;
            expected = Guid.Empty;
            Assert.AreEqual(expected, NutStringExtensions.ToGuid(source, defaults));

            var newGuid = Guid.NewGuid();
            source = newGuid.ToString();
            defaults = null;
            expected = newGuid;
            Assert.AreEqual(expected, NutStringExtensions.ToGuid(source, defaults));

            newGuid = Guid.NewGuid();
            source = newGuid.ToString();
            defaults = Guid.NewGuid();
            expected = newGuid;
            Assert.AreEqual(expected, NutStringExtensions.ToGuid(source, defaults));
        }

        [TestCase(null, TestStringEnum.Default)]
        [TestCase("", TestStringEnum.Default)]
        [TestCase(" ", TestStringEnum.Default)]
        [TestCase("Default", TestStringEnum.Default)]
        [TestCase("One", TestStringEnum.One)]
        [TestCase("Two", TestStringEnum.Two)]
        public void TestToEnum(string source, TestStringEnum expected)
        {
            Assert.AreEqual(expected, NutStringExtensions.ToEnum<TestStringEnum>(source));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", false)]
        [TestCase("@", false)]
        [TestCase("a@a", false)]
        [TestCase("a@a.a", false)]
        [TestCase("a@a.ru", true)]
        [TestCase("a-a@a-a.ru", true)]
        [TestCase("b.a-a@a-a.ru", true)]
        [TestCase("b@aaaaaa.rrrrrrr", true)]
        public void TestIsEmail(string source, bool expected)
        {
            Assert.AreEqual(expected, NutStringExtensions.IsEmailFormat(source));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("a", false)]
        [TestCase("+a", false)]
        [TestCase(" ", false)]
        [TestCase("++", false)]
        [TestCase("+(.).", false)]
        [TestCase("1", true)]
        [TestCase("+1", false)]
        [TestCase("+1 111", true)]
        [TestCase("+1 111-11-11", true)]
        [TestCase("+1 (111) 111-11-11", true)]
        [TestCase("+11111111111111111", true)]
        public void TestIsPhoneFormat(string source, bool expected)
        {
            Assert.AreEqual(expected, NutStringExtensions.IsPhoneFormat(source));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", false)]
        [TestCase("a.a", false)]
        [TestCase("a@a.io", false)]
        [TestCase("a.io", false)]
        [TestCase("http://", false)]
        [TestCase("http://.", false)]
        [TestCase("http://..", false)]
        [TestCase("http://../", false)]
        [TestCase("http://?", false)]
        [TestCase("http://??", false)]
        [TestCase("http://??/", false)]
        [TestCase("http://#", false)]
        [TestCase("http://##", false)]
        [TestCase("http://##/", false)]
        [TestCase("http://a.io?q=Spaces should be encoded", false)]
        [TestCase("//", false)]
        [TestCase("//a", false)]
        [TestCase("///a", false)]
        [TestCase("///", false)]
        [TestCase("http:///a", false)]
        [TestCase("rdar://1234", false)]
        [TestCase("h://test", false)]
        [TestCase("http:// shouldfail.com", false)]
        [TestCase(":// should fail", false)]
        [TestCase("ftps://foo.bar/", false)]
        [TestCase("http://.www.foo.bar/", false)]
        [TestCase("http://.www.foo.bar./", false)]
        [TestCase("www.a.io", true)]
        [TestCase("http://a.io", true)]
        [TestCase("https://a.io", true)]
        [TestCase("http://www.a.io", true)]
        [TestCase("https://www.a.io", true)]
        [TestCase("http://a.io.foo_foo", true)]
        [TestCase("http://a.io.foo_foo/", true)]
        [TestCase("http://a.io.foo_foo_(bar)", true)]
        [TestCase("http://a.io.foo_foo_(bar)_(baz)", true)]
        [TestCase("http://a.io/foo/bar/?p=123", true)]
        [TestCase("http://userid:password@example.com:8080", true)]
        [TestCase("http://userid:password@example.com:8080/", true)]
        [TestCase("http://userid@example.com", true)]
        [TestCase("http://userid@example.com/", true)]
        [TestCase("http://userid:password@example.com", true)]
        [TestCase("http://userid:password@example.com/", true)]
        [TestCase("http://142.42.1.1/", true)]
        [TestCase("ftp://a.io/bar", true)]
        [TestCase("http://1337.net", true)]
        [TestCase("http://a.b-c.de", true)]
        [TestCase("http://223.255.255.254", true)]
        public void TestIsUrlFormat(string source, bool expected)
        {
            Assert.AreEqual(expected, NutStringExtensions.IsUrlFormat(source));
        }
    }
}