using System;
using Nut.Core.Platform;
using Nut.Core.Tests.Base;
using Nut.Core.Tests.Mocks;
using NUnit.Framework;

namespace Nut.Core.Tests.TestDependencies
{
    public class TestSingleton : TestBase
    {
        [Test]
        public void TestInstanceCreation()
        {
            var mockSingleton = new MockSingleton();
            Assert.NotNull(NutSingleton<IMockSingleton>.Instance);

            mockSingleton.Dispose();
        }

        [Test]
        public void TestCantCreateSeveralInstances()
        {
            var mockSingleton = new MockSingleton();
            Assert.Throws<Exception>(() => new MockSingleton());

            mockSingleton.Dispose();
        }

        [Test]
        public void TestDisposeDropInstance()
        {
            var mockSingleton = new MockSingleton();
            mockSingleton.Dispose();

            Assert.IsNull(NutSingleton<IMockSingleton>.Instance);
        }
    }
}