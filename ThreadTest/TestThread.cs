using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest
{
    class TestThread
    {
        private AutoResetEvent a1;
        public void initAndStart()
        {
            Console.WriteLine("PGM Started");
            a1 = new AutoResetEvent(false);

            Thread t1 = new Thread(new ThreadStart(T1Callback));
            t1.Start();

            Console.WriteLine("Thread Started");

            a1.Set();
            Console.WriteLine("a1 set");
            a1.Set();
            Console.WriteLine("a1 set");
            a1.Set();
            Console.WriteLine("a1 set");
            a1.Set();
            Console.WriteLine("a1 set");
            a1.Set();
            Console.WriteLine("a1 set");

            Console.ReadLine();
            
        }

        private void T1Callback()
        {
            while (true)
            {
                a1.WaitOne();
                Console.WriteLine("Executed T1Callback");

                Thread.Sleep(1000);
            }
        }
    }
}
