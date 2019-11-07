using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcurrentTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string someValue = "";

        ManualResetEvent mreExit = new ManualResetEvent(false);
        AutoResetEvent areInterrup = new AutoResetEvent(false);
        ConcurrentQueue<KeyValuePair<string, string>> que = new ConcurrentQueue<KeyValuePair<string, string>>();
        private void button1_Click(object sender, EventArgs e)
        {
            for( int i = 0; i < 10; i++ )
            {
                que.Enqueue(new KeyValuePair<string, string>($"Key{i}", $"Value{i}"));
            }
            someValue = "Button1";
            new Thread(new ThreadStart(() => 
            {
                KeyValuePair<string, string> localValue;
                while(!mreExit.WaitOne(500))
                {
                    if (que.TryDequeue(out localValue))
                    {
                        Console.WriteLine($"{localValue.Key} = {localValue.Value}");

                        if (localValue.Value.Contains("3"))
                        {
                            //send some message.
                            Console.WriteLine("Wait for button3 action.");
                            someValue = "TTTTT";
                            areInterrup.WaitOne(-1);

                            Console.WriteLine(someValue);
                        }
                    }
                }
            })).Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            someValue = "Button2";
            for (int i = 0; i < 10; i++)
            {
                que.Enqueue(new KeyValuePair<string, string>($"KeyAdd{i}", $"ValueAdd{i}"));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mreExit.Set();
            areInterrup.Set();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            someValue = "Button3";

            KeyValuePair<string, string> localValue;
            while (que.TryDequeue(out localValue)) { }

            for (int i = 0; i < 10; i++)
            {
                que.Enqueue(new KeyValuePair<string, string>($"InterrupAdd{i}", $"InterrupAdd{i}"));
            }

            areInterrup.Set();
        }
    }

    
}
