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
            string input = "�ƹ������濡���Ŵٳ��¹����Ծ��� ����̴� �Ϳ���. �Ϳ��� �����̴� �׷���\n�ƹ����� �濡 ���Ŵ�.\n���Ŵ�.����. ����.�����.";
            Paragraph paragraph = new Paragraph(input);
            paragraph.Print();
        }

        [TestMethod]
        public void Test2()
        {
            string input = "����̾߿�";
            Paragraph paragraph = new Paragraph(input);
            paragraph.Print();
        }

    }
}
