using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestFile
{
    class Program
    {
        static void Main(string[] args)
        {
            //string strFullPath = @"C:\Oven\Recipe Files\";

            //DirectoryInfo dir = new DirectoryInfo(strFullPath);

            //FileInfo[] files = dir.GetFiles("*.JOB");
            //foreach( FileInfo file in files)
            //{
            //    //file.IsReadOnly = false;

            //    byte[] rtn = File.ReadAllBytes(file.FullName);
            //    string rtn2 = ByteArrayToString(rtn);

            //    byte[] rtn3 = StringToByteArray(rtn2);

            //    for( int i = 0; i < rtn.Length; i++ )
            //    {
            //        if (rtn[i] != rtn3[i]) Console.WriteLine(i + "BAD");
            //    }
            //}


            //FileInfo fi = new FileInfo(@"C:\SSSS.txt");
            //fi.Delete();

            //Console.ReadLine();

            FileInfo fi = new FileInfo("test");

            double kkkk = 1.234d;
            string test = kkkk.ToString("F1");

        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

    }
}
