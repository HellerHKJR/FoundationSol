using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UsingTabControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //1. Add TabControl
            //2. Modify property to Lock(true) witch make it not movible.

            //Nothings....
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sel = tabControl1.SelectedIndex;

            if (!(++sel < tabControl1.TabCount)) sel = 0;

            tabControl1.SelectedIndex = sel;

        }
    }
}
