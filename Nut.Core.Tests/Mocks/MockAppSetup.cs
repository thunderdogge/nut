using Castle.Core.Logging;
using Nut.Core.Application;

namespace Nut.Core.Tests.Mocks
{
    public class MockAppSetup : NutApplicationSetup
    {
        public MockAppSetup()
        {
            var logger = new MockLogger();
        }
    }
}