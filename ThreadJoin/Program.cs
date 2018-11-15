using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadJoin
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkerThread wt = new WorkerThread();
            Thread th1 = new Thread(new ParameterizedThreadStart(wt.DoWork));
            Thread th2 = new Thread(new ParameterizedThreadStart(wt.DoWork));

            th1.Start(1);
            //th1.Join();
            th2.Start(2);
            //th2.Join();

            Console.Read();
        }
    }


    class WorkerThread
    {
        ManualResetEvent mre = new ManualResetEvent(false);
        public void DoWork(object id)
        {
            for(int i = 0; i < 10; i++)
            {
                mre.WaitOne(100);
                Console.WriteLine("Worker{0} {1}", id, i + 1);
            }
        }
    }
}
