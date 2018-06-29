using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMonitor k = new TestMonitor();
            k.Init();

            Console.ReadLine();
            k.Dispose();
        }
    }


    class TestMonitor : IDisposable
    {
        private enum eTypeSome
        {
            Type1, Type2, Type3, SpecialDeadLock
        }
        private List<KeyValuePair<eTypeSome, object>> queue;
        private static readonly object objectMonitor = new object();
        private bool bExitThread = false;

        private AutoResetEvent are = new AutoResetEvent(false);

        internal void Init()
        {
            queue = new List<KeyValuePair<eTypeSome, object>>();

            MakeThread();
            MakeQueue();
            MakeQueue();

        }

        private void MakeQueue()
        {
            MakeQueueItem(eTypeSome.Type3, 1);
            //Thread.Sleep(100);
            MakeQueueItem(eTypeSome.Type2, "qwer1");
            //Thread.Sleep(100);
            MakeQueueItem(eTypeSome.Type1, true);
            //Thread.Sleep(100);

            MakeQueueItem(eTypeSome.Type3, 2);
            //Thread.Sleep(5000);
            MakeQueueItem(eTypeSome.Type2, "qwer2");
            //Thread.Sleep(100);
            MakeQueueItem(eTypeSome.Type1, false);
            //Thread.Sleep(100);

            MakeQueueItem(eTypeSome.Type3, 3);
            //Thread.Sleep(100);
            MakeQueueItem(eTypeSome.Type2, "qwer3");
            //Thread.Sleep(100);
            MakeQueueItem(eTypeSome.Type1, true);

            //Thread.Sleep(1000);
            bExitThread = true;
        }

        private void MakeQueueItem(eTypeSome type, object v)
        {
            Monitor.Enter(objectMonitor);
            Monitor.Enter(objectMonitor);
            Monitor.Enter(objectMonitor);
            try
            {
                queue.Add(new KeyValuePair<eTypeSome, object>(type, v));

                //if (type != eTypeSome.SpecialDeadLock)
                //    MakeQueueItem(eTypeSome.SpecialDeadLock, 1);
            }            
            finally
            {
                Monitor.Exit(objectMonitor);
                Monitor.Exit(objectMonitor);
                Monitor.Exit(objectMonitor);
            }
            
            
        }

        private void MakeThread()
        {
            bool bExecute = false;
            int tmpCount = 1;
            new Thread(new ThreadStart(() => {
                while (!are.WaitOne(200))
                {
                    //Thread.Sleep(200);
                    //Monitor.Wait(objectMonitor, 200);
                    Monitor.Enter(objectMonitor);
                    try
                    {
                        if(queue.Count > 0) Console.WriteLine("Entered Loop " + tmpCount++);
                        //for(   int i = 0; i < queue.Count; i++ )
                        //IEnumerator<KeyValuePair<eTypeSome, object>> iter = queue.GetEnumerator();

                        //foreach ( KeyValuePair< eTypeSome, object> tmp3 in iter)

                        queue.RemoveAll( a => {
                            Console.WriteLine(a.Value);
                            return a.Key == eTypeSome.Type1;
                        } );

                        //while(iter.MoveNext())
                        //{
                        //    if (are.WaitOne(1))
                        //    {
                        //        are.Set();
                        //        break;
                        //    }
                            
                        //    //KeyValuePair<eTypeSome, object> tmp2 = queue.Peek();

                        //    if ( iter.Current.Key == eTypeSome.Type1 )
                        //    {
                        //        //KeyValuePair<eTypeSome, object> tmp = queue.Dequeue();
                        //        Console.WriteLine(iter.Current.Key.ToString() + " : " + iter.Current.Value.ToString());
                                
                        //        queue.RemoveAll( (iter.Current);
                        //    }
                        //    else
                        //    {
                        //        //KeyValuePair<eTypeSome, object> tmp = queue.Dequeue();

                        //        //Console.WriteLine(tmp.Key.ToString() + " : " + tmp.Value.ToString());
                        //    }
                        //}
                        
                    }
                    finally
                    {
                        Monitor.Exit(objectMonitor);
                    }
                }
            })).Start();
        }

        public void Dispose()
        {
            bExitThread = true;

            are.Set();
        }
    }
}
