using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            ushort[] HoldingRegisterList = { 123, 234, 356, 488 };
            string retLog = string.Empty;
            foreach (ushort memData in HoldingRegisterList)
            {
                retLog += string.Format("{0,5:X4},", memData);
            }

            Console.WriteLine(retLog, "A");






            string testStr = "abcd";
            Console.WriteLine("Result: " + testStr.IndexOfAny(new char[] { 'e', 'd' }));

            string test2 = "123";
            int ret2 = Convert.ToInt32(test2, 10);
            Console.WriteLine("Result: 0x{0:X}", ret2+10);

            string testLotInfo = "LOT_INFO";

            switch(testLotInfo)
            {
                case nameof(eTest.LOT_INFO):
                    Console.WriteLine(testLotInfo);
                    break;
            }


            string testRecipe = "ttt.job.tmp";
            Console.WriteLine(testRecipe.ToUpper().Replace(".tmp", ""));
            Console.WriteLine(testRecipe.ToUpper().Replace(".TMP", ""));

            List<string> tmpList = new List<string>() { "A", "C" };
            Console.WriteLine(string.Join( ",", tmpList.ToArray()));


            Console.WriteLine(BitConverter.ToString(new byte[] { 1, 10, 20, 30, 40 }).Replace('-', ' '));


            string rtn = string.Empty;
            byte[] raw = new byte[] { 0x00, 0x00, 0x81, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x04, 0xC6 };
            byte[] SystemBytes = raw.Skip(6).Take(4).ToArray();

            rtn += "SEND " + BitConverter.ToString(raw).Replace('-', ' ') + " ";
            rtn += string.Format("[Len={0}] [SB={1}] Select.req", raw.Length,
                BitConverter.ToInt32(SystemBytes.Reverse().ToArray(), 0));

            Console.WriteLine(rtn);



            Console.ReadLine();
        }
    }
}
