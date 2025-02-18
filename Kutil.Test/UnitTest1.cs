using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kutil;
using Kutil.Formats;
using static Kutil.G2P;
using System.Diagnostics;
using Kutil.Formats.Korean;
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

        [TestMethod]
        public void TestEogan()
        {
            string currentPath = Environment.CurrentDirectory;
            string input = "¸Ô¾î°£´Ù"; // ¸Ô-, °¡-
            int expected = 2;
            int actual;
            new Word(input).HasYongeonEogan(out actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
