using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadEventCheck
{
    public partial class Form1 : Form
    {

        private ManualResetEvent mreStart1 = new ManualResetEvent(false);
        private AutoResetEvent areStart1 = new AutoResetEvent(false);
        public Form1()
        {
            InitializeComponent();

            new Thread(new ThreadStart(() => {

                while ( !areStart1.WaitOne(0) )
                {
                    
                }

                areStart1.WaitOne(5);

                Console.WriteLine("ARE released");

            })).Start();
            new Thread(new ThreadStart(() => {
                if( mreStart1.WaitOne(-1) )
                {
                    areStart1.Set();
                }

            })).Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mreStart1.Set();
        }
    }
}
