using Moq;
using Nut.Core.Bindings;
using Nut.Core.Bindings.Exceptions;
using Nut.Core.Tests.Base;
using Nut.Core.Tests.Constraints;
using Nut.Core.Tests.Mocks;
using NUnit.Framework;

namespace Nut.Core.Tests.TestBindings
{
    public class TestBindingChain : TestBase
    {
        private Mock<INutBindingStore> mockBindingStore;
        private IMockBindingTarget mockBindingTarget;
        private Mock<INutBindingCreator> mockBindingCreator;

        public override void Setup()
        {
            base.Setup();

            mockBindingStore = new Mock<INutBindingStore>();
            mockBindingTarget = Mock.Of<IMockBindingTarget>();
            mockBindingCreator = new Mock<INutBindingCreator>();
        }

        [Test]
        public void TestValidation()
        {
            mockBindingStore.Setup(x => x.BindingContext).Returns(() => new NutBindingContext());

            var chain = new NutBindingChain<IMockBindingTarget, IMockBindingSource>(mockBindingStore.Object, null, mockBindingCreator.Object);
            Assert.Throws<NutBindingException>(() => chain.RegisterBinding());

            chain = new NutBindingChain<IMockBindingTarget, IMockBindingSource>(mockBindingStore.Object, mockBindingTarget, mockBindingCreator.Object);
            Assert.Throws<NutBindingException>(() => chain.RegisterBinding());

            chain = new NutBindingChain<IMockBindingTarget, IMockBindingSource>(mockBindingStore.Object, mockBindingTarget, mockBindingCreator.Object);
            chain = chain.For(x => x.TargetProperty);
            Assert.Throws<NutBindingException>(() => chain.RegisterBinding());

            chain = new NutBindingChain<IMockBindingTarget, IMockBindingSource>(mockBindingStore.Object, mockBindingTarget, mockBindingCreator.Object);
            chain = chain.To(x => x.SourceProperty);
            Assert.Throws<NutBindingException>(() => chain.RegisterBinding());

            chain = new NutBindingChain<IMockBindingTarget, IMockBindingSource>(mockBindingStore.Object, mockBindingTarget, mockBindingCreator.Object);
            chain = chain.For(x => x.TargetProperty);
            chain = chain.To(x => x.SourceProperty);
            Assert.DoesNotThrow(() => chain.RegisterBinding());
        }

        [Test]
        public void TestBindingContextStringValues()
        {
            var chain = new NutBindingChain<IMockBindingTarget, IMockBindingSource>(mockBindingStore.Object, mockBindingTarget, mockBindingCreator.Object);
            chain = chain.For("TargetProperty");
            chain = chain.To("SourceProperty");
            chain = chain.TwoWay();
            chain = chain.WithConversion("SomeConverter");

            var expected = new NutBindingDescription
            {
                SourceProperty = "SourceProperty",
                TargetProperty = "TargetProperty",
                ConverterName = "SomeConverter",
                Mode = NutBindingMode.TwoWay
            };
            Assert.That(expected, ObjectIs.EqualByProperties(chain.BindingDescription));
        }

        [Test]
        public void TestBindingContextExpressionValues()
        {
            var chain = new NutBindingChain<IMockBindingTarget, IMockBindingSource>(mockBindingStore.Object, mockBindingTarget, mockBindingCreator.Object);
            chain = chain.For(x => x.TargetProperty);
            chain = chain.To(x => x.SourceProperty);
            chain = chain.TwoWay();
            chain = chain.WithConversion("SomeConverter");

            var expected = new NutBindingDescription
            {
                SourceProperty = "SourceProperty",
                TargetProperty = "TargetProperty",
                ConverterName = "SomeConverter",
                Mode = NutBindingMode.TwoWay
            };
            Assert.That(expected, ObjectIs.EqualByProperties(chain.BindingDescription));
        }
    }
}