using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PropertiesTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label1.Text = Properties.Settings.Default.TestV1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.TestV1 = "RRR";
            Properties.Settings.Default.Save();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //reload
            label1.Text = Properties.Settings.Default.TestV1;
        }
    }
}
