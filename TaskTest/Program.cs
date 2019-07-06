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
            List<Task> ts = new List<Task>();
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            Task t1 = Task.Run(() =>
            {
                for (int s = 0; s < 10; s++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"T1, {s}");

                    ct.ThrowIfCancellationRequested();
                }

            }, ct);

            Task t2 = Task.Run(() =>
            {
                for (int s = 0; s < 10; s++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"T2, {s}");

                    ct.ThrowIfCancellationRequested();
                }

            }, ct);

            ts.Add(t1);
            ts.Add(t2);


            if (!Task.WaitAll(ts.ToArray(), 2000))
            {
                tokenSource2.Cancel();
                Console.WriteLine("The timeout interval elapsed.");
                //doTerminate = true;
                //Environment.Exit(99);
            }
            else Console.WriteLine("Success All");

            ////try
            ////{
            ////    for (int i = 0; i < 10; i++)
            ////    {
            ////        Console.WriteLine(i + ": " + (string.IsNullOrWhiteSpace(ts[i].Result) ? "A" : ts[i].Result));
            ////    }
            ////}
            ////catch (AggregateException ex)
            ////{
            ////    Console.WriteLine("TEST22222>> " + ex);
            ////}


            Console.WriteLine("End Test");

            Console.ReadLine();
        }
    }
}
