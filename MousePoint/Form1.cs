using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MousePoint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool isDown = false;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if( e.Button == MouseButtons.Right) isDown = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if( isDown )
            {
                Console.WriteLine("{0},{1},{2},{3}", e.X, e.Y, e.Location.X, e.Location.Y );
            }            
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }
    }
}
