using System;
using Nut.Core.Environment;
using Nut.Core.Tests.Base;
using Nut.Core.Tests.Mocks;
using NUnit.Framework;

namespace Nut.Core.Tests.TestPlatform
{
    public class TestPreferences : TestBase
    {
        [Test]
        public void TestGuid()
        {
            var preferences = new MockPreferences();

            var id1 = Guid.Empty;
            var id2 = Guid.NewGuid();
            preferences.PutGuid("key1", id1);
            preferences.PutGuid("key2", id2);

            Assert.AreEqual(id1, preferences.GetGuid("key1"));
            Assert.AreEqual(id2, preferences.GetGuid("key2"));
            Assert.AreEqual(Guid.Empty, preferences.GetGuid("key3"));
        }

        [Test]
        public void TestEnum()
        {
            var preferences = new MockPreferences();

            preferences.PutEnum("key1", MockEnum.One);
            preferences.PutEnum("key2", MockEnum.Four);

            Assert.AreEqual(MockEnum.One, preferences.GetEnum<MockEnum>("key1"));
            Assert.AreEqual(MockEnum.Four, preferences.GetEnum<MockEnum>("key2"));
            Assert.AreEqual(MockEnum.Default, preferences.GetEnum<MockEnum>("key3"));
        }

        [Test]
        public void TestDateTime()
        {
            var preferences = new MockPreferences();

            var date1 = new DateTime(2015, 1, 1, 1, 1, 1, DateTimeKind.Utc);
            var date2 = new DateTime(2015, 2, 2, 2, 2, 2, DateTimeKind.Local);
            var date3 = new DateTime(2015, 3, 3, 3, 3, 3, DateTimeKind.Unspecified);

            preferences.PutDateTimeUtc("key1", date1);
            preferences.PutDateTimeLocal("key2", date2);
            preferences.PutDateTime("key3", date3, DateTimeKind.Unspecified);

            Assert.AreEqual(date1, preferences.GetDateTimeUtc("key1"));
            Assert.AreEqual(date2, preferences.GetDateTimeLocal("key2"));
            Assert.AreEqual(date3, preferences.GetDateTime("key3", null, DateTimeKind.Unspecified));
            Assert.AreEqual(null, preferences.GetDateTime("key4", null, DateTimeKind.Unspecified));
        }
    }
}