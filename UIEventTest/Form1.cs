using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEventTest
{
    public partial class Form1 : Form
    {
        event EventHandler<Temp1> testMyEvent;
        public Form1()
        {
            InitializeComponent();
            testMyEvent += Form1_testMyEvent;
        }

        private void Form1_testMyEvent(object sender, Temp1 e)
        {
            Console.WriteLine(e.RESA);
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {   
            testMyEvent.Invoke(this, new Temp1("SWE"));
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }

    public class Temp1 : EventArgs
    {
        public string RESA { get; set; }

        public Temp1(string K)
        {
            this.RESA = K;
        }
    }


}
