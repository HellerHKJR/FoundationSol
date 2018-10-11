using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("TEST {0}", 1234);


            //byte k = 0x11;
            //if( k.GetType() == typeof(byte) )
            //{
            //    Console.WriteLine("{0}", k);
            //}

            ////Console.Write("{0}", GetValue<string>(1234));

            //Console.WriteLine(  (string)(Convert.ChangeType("abcd", typeof(string)))  );

            //Type t = typeof(string);

            //int ss = GetValue<int>("1234");

            //for( int i = 1; i <= 20000000; i++ )
            //{
            //    Console.Write(i + "\t");
            //}

            string testRecipeFullPathwithoutExt = @"PP-Select_Test_Folder\Recipe Files\SERVER_RECIPE";
            //string testRecipeFullPathwithoutExt = @"K:\MS Lee\PP-Select_Test_Folder\Recipe Files\SERVER_RECIPE";
            int pos = testRecipeFullPathwithoutExt.LastIndexOf('\\');
            string ret = testRecipeFullPathwithoutExt.Substring(0, pos);
            DirectoryInfo tmpDI = new DirectoryInfo(ret);

            Hashtable ht = new Hashtable();
            ht.Add(1, "1");
            ht.Add(100, "100");
            ht.Add(2, "2");
            ht.Add(5, "5");

            IDictionaryEnumerator iter = ht.GetEnumerator();

            foreach ( int k in ht.Keys)
            {
                Console.WriteLine(ht[k]);
            }

            Console.WriteLine();
            Console.WriteLine();

            Dictionary<object, object> dic = new Dictionary<object, object>();
            dic.Add(1, "1");
            //dic.Add(1, "1");
            dic.Add(100, "100");
            dic.Add(2, "2");
            dic.Add(5, "5");

            IEnumerator iterDic = dic.GetEnumerator();
            iterDic.MoveNext();
            //IDictionary<string, object>.Equals iterDic = dic.GetEnumerator();


            foreach (int k in dic.Keys)
            {
                Console.WriteLine(dic[k]);
            }

            //PairValue pv = new PairValue() { "a", "1" };
            ArrayList list = new ArrayList() { new Hashtable() { { "a", "1" } }, new Hashtable() { { "b", "2" } } };

            List<Hashtable> l2 = new List<Hashtable> { new Hashtable { { "A", "1" } } };//, { "A", "1" } };


            double dv = 123.11;
            int iv = 321;

            object tmp;

            tmp = dv;

            Console.WriteLine(tmp.ToString());
            //Console.WriteLine((string)tmp);
            tmp = Convert.ChangeType(tmp, typeof(string));
            Console.WriteLine((string)tmp);

            FileInfo fi = new FileInfo("abcd.abc");

            Ackc7 ss = Ackc7.tt;
            Console.WriteLine((long)ss);

            List<string> r1 = new List<string>() { "A", "b", "C" };
            List<string> r2 = new List<string>() { "D", "E" };
            List<string> rSum = new List<string>();
            r1.AddRange(r2);// .Concat(r2) as List<string>;

            rSum = Enumerable.Repeat("5", 100).ToList<string>();

            //string rSum2 = Enumerable.Repeat("2", 5);

            string[] sss = Enum.GetNames(typeof(System.IO.Ports.SerialData));
            Array test1 = Enum.GetValues(typeof(System.IO.Ports.SerialData));
            string[] sss2 = Enum.GetNames(typeof(eBaudRate));
            Array test12 = Enum.GetValues(typeof(eBaudRate));
            //System.IO.Ports.
            string[] sss3 = ConvertToArray(typeof(eBaudRate));


            Dictionary<int, int> tmpHash = new Dictionary<int, int>();
            tmpHash.Add(101, 123456);
            tmpHash.Add(201, 234567);
            tmpHash.Add(50, 3456);

            foreach( int sk in tmpHash.Keys )
            {
                Console.WriteLine(sk);

            }



            object tmpF = null;
            float tmpFF = 23.3f;

            tmpF = tmpFF;

            Console.WriteLine(tmpF);

            Console.WriteLine(bool.TrueString);
            Console.WriteLine(bool.FalseString);
            Console.WriteLine(Convert.ChangeType(int.Parse("1"), TypeCode.Boolean) );

            Console.WriteLine(Convert.ToBoolean("0"));



            Console.ReadLine();

        }

        private static string[] ConvertToArray(Type type)
        {
            List<string> rtn = new List<string>();

            Array some = Enum.GetValues(type);
            foreach (int item in some)
            {
                rtn.Add(item.ToString());

            }

            return rtn.ToArray();
        }

        enum eBaudRate
        {
            x9600 = 9600,
            x14400 = 14400,
            x19200 = 19200,
            x38400 = 38400
        }

        public enum Ackc7 : long
        {
            tt = 0x40
        }


        //public class PairValue : IEnumerable<KeyValuePair>
        //{
        //    public string key;
        //    public object value;

        //    public IEnumerator GetEnumerator()
        //    {
        //        return null;                
        //    }

        //    public void Add(string k) => key = k;
        //    public void Add(object v) => value = v;
        //    //public void Add(object v) => value = v;
        //}

        public static T GetValue<T>(object k)
        {
            T rtnValue = default(T);
            try
            {
                string rtnString = string.Empty;
                if (k is int)
                {
                    rtnString = k.ToString();
                }
                else if (k is string)
                {
                    rtnString = (string) k;
                }

                if (k.GetType() == typeof(T))
                {
                    rtnValue = (T)(Convert.ChangeType(rtnString, typeof(T)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rtnValue;
        }
    }

}
