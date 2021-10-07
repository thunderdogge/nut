using Nut.Core.Models;

namespace Nut.Core.Tests.Mocks
{
    public class MockViewModel3 : NutViewModel
    {
        public string Property1 { get; set; }

        public string Property1Error { get; set; }

        public string Property2 { get; set; }

        public string Property2Error { get; set; }

        public MockViewModel2 Model2 { get; set; }
    }
}