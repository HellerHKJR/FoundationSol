using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FormatTest
{
    class Program
    {
        enum eTest
        {
            LOT_INFO
        }

        static void Main(string[] args)
        {
            //ushort[] HoldingRegisterList = { 123, 234, 356, 488 };
            //string retLog = string.Empty;
            //foreach (ushort memData in HoldingRegisterList)
            //{
            //    retLog += string.Format("{0,5:X4},", memData);
            //}

            //Console.WriteLine(retLog, "A");






            //string testStr = "abcd";
            //Console.WriteLine("Result: " + testStr.IndexOfAny(new char[] { 'e', 'd' }));

            //string test2 = "123";
            //int ret2 = Convert.ToInt32(test2, 10);
            //Console.WriteLine("Result: 0x{0:X}", ret2+10);

            //string testLotInfo = "LOT_INFO";

            //switch(testLotInfo)
            //{
            //    case nameof(eTest.LOT_INFO):
            //        Console.WriteLine(testLotInfo);
            //        break;
            //}


            //string testRecipe = "ttt.job.tmp";
            //Console.WriteLine(testRecipe.ToUpper().Replace(".tmp", ""));
            //Console.WriteLine(testRecipe.ToUpper().Replace(".TMP", ""));

            //List<string> tmpList = new List<string>() { "A", "C" };
            //Console.WriteLine(string.Join( ",", tmpList.ToArray()));


            //Console.WriteLine(BitConverter.ToString(new byte[] { 1, 10, 20, 30, 40 }).Replace('-', ' '));


            //string rtn = string.Empty;
            //byte[] raw = new byte[] { 0x00, 0x00, 0x81, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x04, 0xC6 };
            //byte[] SystemBytes = raw.Skip(6).Take(4).ToArray();

            //rtn += "SEND " + BitConverter.ToString(raw).Replace('-', ' ') + " ";
            //rtn += string.Format("[Len={0}] [SB={1}] Select.req", raw.Length,
            //    BitConverter.ToInt32(SystemBytes.Reverse().ToArray(), 0));

            //Console.WriteLine(rtn);

            //float t = 123.123f;
            //Console.WriteLine(t.ToString("F1"));
            //float t1 = 123f;
            //Console.WriteLine(t1.ToString("F1"));

            //int kk = 123;
            //Console.WriteLine(kk.ToString(""));

            //testEnum s = testEnum.test1;
            //s = (testEnum)1;
            //Console.WriteLine(s.ToString());

            //TimeSpan
            TimeSpan ts = TimeSpan.FromMinutes(100);
            Console.WriteLine("{0:D2}:{1:D2}", ts.Hours, ts.Minutes);
            Console.WriteLine("{0}", ts.Ticks / (60 * 10 * 1000 * 1000));

            ts = TimeSpan.FromMinutes(5);
            Console.WriteLine("{0:D2}:{1:D2}", ts.Hours, ts.Minutes);

            Console.WriteLine("{0}", ts.Ticks / (60 * 10 * 1000 * 1000));

            //FileInfo di = new FileInfo("C:\\temp\\text.txt");
            //FileInfo di2 = new FileInfo("C:\\temp");

            float a = 0F;
            float b = 0.1F;
            float c = 1.1234567F;
            float d = 1.1F;
            float e = 0.01F;

            Console.WriteLine(a.ToString("#0.0#"));
            Console.WriteLine(b.ToString("#0.0#"));
            Console.WriteLine(c.ToString("#0.0#"));
            Console.WriteLine(d.ToString("#0.0#"));
            Console.WriteLine(e.ToString("#0.0#"));

            DateTime now = DateTime.Now;
            Console.WriteLine(Convert.ToInt32(now.ToString("yy")));
            Console.WriteLine(Convert.ToInt32(now.ToString("MM")));
            Console.WriteLine(Convert.ToInt32(now.ToString("dd")));
            Console.WriteLine(Convert.ToInt32(now.ToString("HH")));
            Console.WriteLine(Convert.ToInt32(now.ToString("mm")));
            Console.WriteLine(Convert.ToInt32(now.ToString("ss")));




            Console.ReadLine();
        }


        enum testEnum
        {
            none,
            test1,
        }
    }
}
