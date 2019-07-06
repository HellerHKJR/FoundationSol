using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomButtonTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int sec = DateTime.Now.Second;
            textBox1.Text = sec.ToString();

            longClickButton41.IsLampOn = sec % 2 == 0;
        }

        private void longClickButton1_OnOff(string tag, bool isOn)
        {
            Console.WriteLine("Button is {0}", isOn? "On" : "Off");
        }
    }
}
