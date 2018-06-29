using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TestLoadAndShown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Load");
            Thread.Sleep(1000);

            this.Visible = false;
            Thread.Sleep(1000);
            this.Visible = true;
            Thread.Sleep(1000);
            this.Visible = false;
            Thread.Sleep(1000);
            this.Visible = true;
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Console.WriteLine("Shown");
        }
    }
}
