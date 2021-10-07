using Nut.Core.Application;
using Nut.Core.Models.Validation;
using Nut.Core.Tests.Base;
using Nut.Core.Tests.Mocks;
using NUnit.Framework;

namespace Nut.Core.Tests.TestSetup
{
    public class TestRegistrations : TestBase
    {
        [Test]
        public void TestResolveUniqueTypes()
        {
            var app = new NutApplication(new MockAppSetup());
            app.Setup();

            var validator1 = Nuts.Resolve<INutValidator>();
            var validator2 = Nuts.Resolve<INutValidator>();

            Assert.AreNotEqual(validator1, validator2);
        }
    }
}