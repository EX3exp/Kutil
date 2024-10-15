using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kutil;
using static Kutil.G2P;
namespace Kutil.Test
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestVariation()
        {
            string input = "³ª´Â  ¹äÀ»";
            string expected = "³ª´Â  ¹äÀ»";
            string actual = Convert(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
