using System;

namespace Nut.Core.Tests.Mocks
{
    public class MockComplexClass : IMockComplexClass
    {
        public MockComplexClass(IMockSimpleClass simpleClass)
        {
            Id = simpleClass.Id;
            Name = simpleClass.Name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public interface IMockComplexClass
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}