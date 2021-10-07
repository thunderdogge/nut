using NUnit.Framework;

namespace Nut.Core.Tests.Base
{
    public abstract class TestBase
    {
        [SetUp]
        public virtual void Setup()
        {
        }

        [TearDown]
        public virtual void TearDown()
        {
        }
    }
}