using System;
using System.Linq.Expressions;
using Nut.Core.Extensions;
using Nut.Core.Tests.Base;
using Nut.Core.Tests.Mocks;
using NUnit.Framework;

namespace Nut.Core.Tests.TestExtensions
{
    public class TestReflectionExtensions : TestBase
    {
        [Test]
        public void TestGetPropertyPath()
        {
            Expression<Func<MockViewModel1, object>> e0 = x => null;
            Assert.AreEqual(null, NutExpressionExtensions.GetPropertyPath(e0));

            Expression<Func<MockViewModel1, object>> e1 = x => true;
            Assert.AreEqual(null, NutExpressionExtensions.GetPropertyPath(e1));

            Expression<Func<MockViewModel1, object>> e2 = x => x.Name;
            Assert.AreEqual("Name", NutExpressionExtensions.GetPropertyPath(e2));

            Expression<Func<MockViewModel1, object>> e3 = x => x.Model2.Name;
            Assert.AreEqual("Model2.Name", NutExpressionExtensions.GetPropertyPath(e3));

            Expression<Func<MockViewModel1, object>> e4 = x => x.Model2.Model1.Model2.Model1.Model2;
            Assert.AreEqual("Model2.Model1.Model2.Model1.Model2", NutExpressionExtensions.GetPropertyPath(e4));

            Expression<Func<MockViewModel1, object>> e5 = x => string.IsNullOrEmpty(x.Model2.Name);
            Assert.AreEqual(null, NutExpressionExtensions.GetPropertyPath(e5));
        }
    }
}