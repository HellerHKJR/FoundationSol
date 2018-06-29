using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TaskTest
{
    class Program
    {
        private AutoResetEvent autoEvent;
        static void Main(string[] args)
        {
            
            Program p = new Program();
            p.runnnn();
            

            Console.ReadLine();
        }

        private void runnnn()
        {
            bool doTerminate = false;
            List<Task<string>> ts = new List<Task<string>>();

                for (int i = 0; i < 10; i++)
                {
                    Task<string> t = Task.Run(() =>
                    {
                        string ret = "AAA";
                        for (int s = 0; s < i; s++)
                        {
                            if (!doTerminate) Thread.Sleep(1000);
                            else break;
                            
                            ret = "R" + s;
                        }

                        return ret;
                    });
                    ts.Add(t);
                }

                TimeSpan timeout = TimeSpan.FromMilliseconds(5000);


                //Task.WaitAll(ts.ToArray());
                if (!Task.WaitAll(ts.ToArray(), timeout))
                {
                    Console.WriteLine("The timeout interval elapsed.");
                    doTerminate = true;
                    //Environment.Exit(99);
                }
                else Console.WriteLine("Success All");
           
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(i + ": " + (string.IsNullOrWhiteSpace(ts[i].Result) ? "A" : ts[i].Result));
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("TEST22222>> " + ex);
            }


            Console.WriteLine("End Test");

            Console.ReadLine();
        }
    }
}
