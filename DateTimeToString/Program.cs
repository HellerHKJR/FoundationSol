using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DateTimeToString
{
    class Program
    {
        static void Main(string[] args)
        {

            //float te = 123.1234567f;
            //Console.WriteLine("{0:0.00}", te);

            TimeSpan tt = TimeSpan.FromSeconds(5431);
            Console.WriteLine("{0}", tt.ToString(@"mm\:ss"));
            //Console.WriteLine("{0}:{1}", Math.Truncate(tt.TotalMinutes).ToString("F0"), tt.Seconds);

            //string[] pass = { "level1", "level2", "level3" };

            //for (int i = 0; i < 3; i++)
            //{
            //    string hash = "";
            //    using (var sha = System.Security.Cryptography.SHA256.Create())
            //    {
            //        var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(pass[i]+" Heller"));
            //        hash = Convert.ToBase64String(computedHash);
            //    }

            //    Console.WriteLine(hash);

            //}


            //for (int i = 0; i < 100; i++)
            //{
            //    DateTime dt = DateTime.Now;
            //    string rtn = string.Format("{0:HH:mm:ss.fff}", dt);

            //    Console.WriteLine(rtn);
            //    Thread.Sleep(500);
            //}

            string strDate1 = "2013 06 15T11:55:33";
            string strDate2 = "2013/06/16T11:55:33";
            string strDate3 = "2013.06.17T11:55:33";
            string strDate4 = "2013.6.8T11:55:33";
            string strDate5 = "4/9/2019T11:55:33";

            DateTime dtExample1 = Convert.ToDateTime((strDate1));
            DateTime dtExample2 = Convert.ToDateTime((strDate2));
            DateTime dtExample3 = Convert.ToDateTime((strDate3));
            DateTime dtExample4 = Convert.ToDateTime((strDate4));
            string msg = dtExample1.ToString("yyyy-MM-dd") + "\r\n" + dtExample2.ToString("yyyy-MM-dd") +
               "\r\n" + dtExample3.ToString("yyyy-MM-dd") + "\r\n" + dtExample4.ToString("yyyy-MM-dd");

            Console.WriteLine(msg);

            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            for( int i = 1; i < 20; i++ )
            {
                int ss = (int)Math.Round(i / 2F, MidpointRounding.AwayFromZero);
                Console.WriteLine($"{i} = {ss}");
            }


            Dictionary<int, string> channelName = new Dictionary<int, string>();
            channelName.Add(1, "AAA");
            channelName.Add(2, "bbb");

            string ret = "";
            try
            {
                ret = channelName.Where(k => k.Key == 1).First().Value;
                ret = channelName.Where(k => k.Key == 3).First().Value;
            }
            catch
            {

            }
            finally
            {

            }

            List<TTTT> testing = new List<TTTT>();
            for( int i = 0; i < 5; i++)
            {
                testing.Add(new TTTT() { Serial = i+1, Name = $"Test{i+1}"});
            }

            var tes = testing.SelectMany(k => k.Name);

            Console.WriteLine(ret);
            var tes2 = tes.ToArray();


            for( int i = 0; i < 10; i++)
            {
                int j = i / 2 + 1;
                
            }




            DateTime dt = DateTime.Now;
            string sFileName = string.Format($"{dt.Month:00}-{dt.Day:00}.csv");

            string[] ssss = { TestEnum.ABC1.ToString(), TestEnum.ABC2.ToString(), TestEnum.ABC3.ToString() };
            var list = new List<string> { "A", "B","C", "D" };  //MUST
            var list2 = new List<string> { "A", "C", "B" };     //RECV

            var query = list.Intersect(list2);
            int tre = query.Count();

            DateTime dtExample5 = Convert.ToDateTime(strDate5);

            Console.ReadLine();


        }
    }

    public enum TestEnum
    {
        ABC1, ABC2, ABC3
    }

    public class TTTT
    {
        public int Serial { get; set; }
        public string Name { get; set; }
    }
}
