using System;
using Nut.Core.Dependencies;
using Nut.Core.Platform;
using Nut.Core.Tests.Base;
using Nut.Core.Tests.Mocks;
using NUnit.Framework;

namespace Nut.Core.Tests.TestDependencies
{
    public class TestResolve : TestBase
    {
        public override void TearDown()
        {
            base.TearDown();

            NutSingleton<INutIocProvider>.Instance?.Dispose();
        }

        [Test]
        public void TestCreateUnique()
        {
            var iocProvider = new NutIocProvider();
            iocProvider.RegisterServiceDescription(typeof(IMockSimpleClass), x => new MockSimpleClass());

            var instance1 = iocProvider.Create<IMockSimpleClass>();
            instance1.Id = Guid.NewGuid();
            instance1.Name = "Instance1";

            var instance2 = iocProvider.Create<IMockSimpleClass>();
            instance1.Id = Guid.NewGuid();
            instance1.Name = "Instance2";

            Assert.AreNotEqual(instance1.Id, instance2.Id);
            Assert.AreNotEqual(instance1.Name, instance2.Name);
        }

        [Test]
        public void TestResolveSingleton()
        {
            var iocProvider = new NutIocProvider();
            iocProvider.RegisterServiceDescription(typeof(MockSimpleClass), x => new MockSimpleClass());
            iocProvider.RegisterServiceDescription(typeof(IMockSimpleClass), x => x(typeof(MockSimpleClass)));

            var instance1 = iocProvider.Resolve<IMockSimpleClass>();
            instance1.Id = Guid.NewGuid();
            instance1.Name = "Instance1";

            var instance2 = iocProvider.Resolve<IMockSimpleClass>();
            instance1.Id = Guid.NewGuid();
            instance1.Name = "Instance2";

            Assert.AreEqual(instance1.Id, instance2.Id);
            Assert.AreEqual(instance1.Name, instance2.Name);
        }

        [Test]
        public void TestResolveUnique()
        {
            var iocProvider = new NutIocProvider();
            iocProvider.RegisterServiceDescription(typeof(MockSimpleClass), x => new MockSimpleClass());
            iocProvider.RegisterServiceDescription(typeof(IMockSimpleClass), x => x(typeof(MockSimpleClass)));
            iocProvider.RegisterUnique<IMockSimpleClass, MockSimpleClass>();

            var instance1 = iocProvider.Resolve<IMockSimpleClass>();
            instance1.Id = Guid.NewGuid();
            instance1.Name = "Instance1";

            var instance2 = iocProvider.Resolve<IMockSimpleClass>();
            instance1.Id = Guid.NewGuid();
            instance1.Name = "Instance2";

            Assert.AreNotEqual(instance1.Id, instance2.Id);
            Assert.AreNotEqual(instance1.Name, instance2.Name);
        }

        [Test]
        public void TestResolveFactory()
        {
            var iocProvider = new NutIocProvider();
            iocProvider.RegisterFactory<IMockSimpleClass>(() => new MockSimpleClass());

            Assert.NotNull(iocProvider.Resolve<IMockSimpleClass>());
        }

        [Test]
        public void TestTryResolve()
        {
            var iocProvider = new NutIocProvider();
            iocProvider.RegisterServiceDescription(typeof(MockSimpleClass), x => new MockSimpleClass());
            iocProvider.RegisterServiceDescription(typeof(IMockSimpleClass), x => x(typeof(MockSimpleClass)));

            IMockSimpleClass simpleClass;
            Assert.IsTrue(iocProvider.TryResolve(out simpleClass));
            Assert.NotNull(simpleClass);

            IMockComplexClass complexClass;
            Assert.IsFalse(iocProvider.TryResolve(out complexClass));
            Assert.IsNull(complexClass);
        }

        [Test]
        public void TestSafeResolve()
        {
            var iocProvider = new NutIocProvider();
            Assert.IsNull(iocProvider.SafeResolve<IMockSimpleClass>());
        }

        [Test]
        public void TestDependencyResolve()
        {
            var iocProvider = new NutIocProvider();
            iocProvider.RegisterServiceDescription(typeof(MockSimpleClass), x => new MockSimpleClass());
            iocProvider.RegisterServiceDescription(typeof(IMockSimpleClass), x => x(typeof(MockSimpleClass)));
            iocProvider.RegisterServiceDescription(typeof(MockComplexClass), x => new MockComplexClass((IMockSimpleClass)x(typeof(IMockSimpleClass))));
            iocProvider.RegisterServiceDescription(typeof(IMockComplexClass), x => x(typeof(MockComplexClass)));

            Assert.NotNull(iocProvider.Resolve<IMockSimpleClass>());
            Assert.NotNull(iocProvider.Resolve<MockSimpleClass>());
            Assert.NotNull(iocProvider.Resolve<IMockComplexClass>());
            Assert.NotNull(iocProvider.Resolve<MockComplexClass>());
        }

        [Test]
        public void TestNotRegistered()
        {
            var iocProvider = new NutIocProvider();
            iocProvider.RegisterServiceDescription(typeof(MockSimpleClass), null);

            Assert.Throws<InvalidOperationException>(() => iocProvider.Resolve<IMockSimpleClass>());
        }
    }
}