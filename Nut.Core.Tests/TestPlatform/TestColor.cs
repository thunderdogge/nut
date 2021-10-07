using Nut.Core.Platform;
using NUnit.Framework;

namespace Nut.Core.Tests.TestPlatform
{
    public class TestColor
    {
        [Test]
        public void TestDataIntegrity()
        {
            var color = new NutColor(255, 126, 64, 32);

            Assert.AreEqual(255, color.R);
            Assert.AreEqual(126, color.G);
            Assert.AreEqual(64, color.B);
            Assert.AreEqual(32, color.A);
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(255, 255, 255, 255, -1)]
        [TestCase(255, 128, 62, 32, 553615422)]
        public void TestIntConversion(int r, int g, int b, int a, int argb)
        {
            var color = new NutColor(r, g, b, a);
            Assert.AreEqual(argb, color.ARGB);

            color = new NutColor(argb);
            Assert.AreEqual(color.R, r);
            Assert.AreEqual(color.G, g);
            Assert.AreEqual(color.B, b);
            Assert.AreEqual(color.A, a);
        }
    }
}