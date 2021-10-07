using Nut.Core.Models;

namespace Nut.Core.Tests.Mocks
{
    public class MockViewModel1 : NutViewModel
    {
        public string Name { get; set; }

        public MockViewModel2 Model2 { get; set; }
    }
}