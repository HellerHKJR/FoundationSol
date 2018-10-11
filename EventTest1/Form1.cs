using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EventTest1
{
    public partial class Form1 : Form
    {
        
        public SomeSender newSender;
        public SomeReceiver newReceiver;

        public Form1()
        {
            InitializeComponent();

            newSender = new SomeSender();
            newReceiver = new SomeReceiver(newSender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newSender.SenderMethod();
        }
    }


    public class TestEventArg : EventArgs
    {
        public string a;
        public string b;
        public int c;
    }

    public class SomeReceiver
    {
        public SomeReceiver(SomeSender ss)
        {
            ss.OnChangedSomething += Ss_OnChangedSomething;
        }

        private void Ss_OnChangedSomething(object sender, TestEventArg e)
        {
            e.a = "ABCD";
            e.b = "aaaa";
            e.c = 1000;
        }
    }

    public class SomeSender
    {
        public event EventHandler<TestEventArg> OnChangedSomething;
        public SomeSender()
        {
            
        }

        public void SenderMethod()
        {
            TestEventArg tt = new TestEventArg();
            tt.a = "E";
            tt.b = "Q";
            tt.c = 1234;

            Console.WriteLine($"Before Value List : {tt.a}, {tt.b}, {tt.c}");
            OnChangedSomething?.Invoke(this, tt);

            Console.WriteLine($"After Value List : {tt.a}, {tt.b}, {tt.c}");
        }
    }
}
