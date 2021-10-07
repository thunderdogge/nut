using System.Collections;
using System.Collections.Generic;
using Nut.Core.Extensions;
using Nut.Core.Tests.Base;
using NUnit.Framework;

namespace Nut.Core.Tests.TestExtensions
{
    public class TestEnumerableExtensions : TestBase
    {
        private static object[] testCountSource = new[]
        {
            new object[] { null, 0 },
            new object[] { new object[0], 0 },
            new object[] { new[] { 1 }, 1 },
            new object[] { new List<string> { "a", "b" }, 2 }
        };

        [Test]
        [TestCaseSource(nameof(testCountSource))]
        public void TestCount(IEnumerable target, int expectedCount)
        {
            Assert.AreEqual(expectedCount, NutEnumerableExtensions.Count(target));
        }
    }
}