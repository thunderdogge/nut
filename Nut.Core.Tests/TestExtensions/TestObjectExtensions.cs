using Nut.Core.Extensions;
using Nut.Core.Tests.Base;
using NUnit.Framework;

namespace Nut.Core.Tests.TestExtensions
{
    public class TestObjectExtensions : TestBase
    {
        [Test]
        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(-1, false)]
        [TestCase(0.1d, true)]
        [TestCase(10L, true)]
        [TestCase(int.MaxValue, true)]
        [TestCase(long.MaxValue, true)]
        [TestCase(double.MaxValue, true)]
        [TestCase(int.MinValue, false)]
        [TestCase(long.MinValue, false)]
        [TestCase(double.MinValue, false)]
        [TestCase(null, false)]
        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(true, true)]
        [TestCase(typeof(string), true)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("skate", true)]
        public void TestIsTrueValue(object target, bool expected)
        {
            Assert.AreEqual(expected, NutObjectExtensions.IsTrueValue(target));
        }
    }
}