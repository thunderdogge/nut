using System;

namespace Nut.Core.Tests.Mocks
{
    public class MockSimpleClass : IMockSimpleClass
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public interface IMockSimpleClass
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}