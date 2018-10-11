using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TestMonitor2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 첫번째 쓰레드와 두번째 쓰레드가 있을 때, Monitor를 통해서 공유자원을 핸들링하면 서로 경쟁관계의 쓰레드는 Monitoring을 놓치는 일이 없다.
        /// 즉, 상대 쓰레드가 Wait를 하지 않는 이상 자신의 쓰레드만 자원을 활용할 수 있게 된다.
        /// Wait는 Monitor 밖에서 해야 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {
            TestClass a = new TestClass();
            a.CallTestFunc();
        }
    }

    public class DataClass
    {
        public string _data = "AAA";
    }

    public class TestClass
    {
        public delegate void SampleDele(DataClass data);


        object _monitor = new object();
        Dictionary<int, string> dd; 

        public void Init()
        {
            dd = new Dictionary<int, string>();

            for( int i = 0; i < 100; i++)
            {
                dd.Add(i + 1, $"{i+1}번째");
            }

            ManualResetEvent mre = new ManualResetEvent(false);

            new Thread(new ThreadStart(() => {

                for (int h = 0; h < 100; h++)
                {
                    bool isTaken = false;
                    try
                    {
                        //Thread.Sleep(10);
                        mre.WaitOne(1);
                        Monitor.Enter(_monitor, ref isTaken);
                        Console.WriteLine($"첫번째 쓰레드 : {dd[h + 1]}");
                        
                    }
                    finally
                    {
                        if (isTaken) Monitor.Exit(_monitor);
                    }
                }
            })).Start();

            new Thread(new ParameterizedThreadStart((jj) => {

                //SampleDele sampleDele = new SampleDele(jj);
                //Console.WriteLine("--------------------" + jj);

                for (int h = 0; h < 100; h++)
                {
                    bool isTaken = false;
                    try
                    {
                        //Thread.Sleep(10);
                        mre.WaitOne(1);
                        Monitor.Enter(_monitor, ref isTaken);

                        
                        //if (h == 50) mre.WaitOne(10000);
                        Console.WriteLine($"두번째 쓰레드 : {dd[h + 1]}");
                    }
                    finally
                    {
                        if (isTaken) Monitor.Exit(_monitor);
                    }
                }
            })).Start(100);

        }

        public void SomeChildFunc(DataClass data)
        {
            Console.WriteLine("===============================^^=========================");
            Console.WriteLine($"^^^{data._data}^^^");
        }

        public void TestFunc(SampleDele dele)
        {
            dele(new DataClass());
        }

        public void CallTestFunc()
        {            
            TestFunc(SomeChildFunc);
        }

    }

}
