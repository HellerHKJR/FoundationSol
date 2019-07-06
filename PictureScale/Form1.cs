using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureScale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            numericUpDown1.Value = 30;  //xpos
            numericUpDown2.Value = 70;  //xlength
            numericUpDown3.Value = 30;  //ypos
            numericUpDown4.Value = 30;  //ygap
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.Black, 2);

            float xbase = (float)numericUpDown1.Value;
            float xLength = (float)numericUpDown2.Value;
            float ybase = (float)numericUpDown3.Value;
            float yGap = (float)numericUpDown4.Value;

            //temporary slot count regarding tray picture
            for (int i = 0; i < 13; i++)
            {
                e.Graphics.DrawLine(myPen, new PointF(xbase, (i * yGap + ybase) ), new PointF(xbase + xLength, (i * yGap + ybase) ));

            }

            myPen.Dispose();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }
    }
}
