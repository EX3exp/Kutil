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
        public void Test1()
        {
            string input = "아버지가방에들어가신다나는밥을먹었다 고양이는 귀엽다. 귀엽기 때문이다 그렇다\n아버지가 방에 들어가신다.\n들어가신다.들어가다. 들어갔다.고양이.";
            Paragraph paragraph = new Paragraph(input);
            paragraph.Print();
        }

        [TestMethod]
        public void Test2()
        {
            string input = "고양이야옹";
            Paragraph paragraph = new Paragraph(input);
            paragraph.Print();
        }

    }
}
