using Nut.Core.Models;

namespace Nut.Core.Tests.Mocks
{
    public class MockViewModel2 : NutViewModel
    {
        public string Name { get; set; }

        public MockViewModel1 Model1 { get; set; }
    }
}