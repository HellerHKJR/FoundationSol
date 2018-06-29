using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DisposeTest
{
    public partial class Form1 : Form, IDisposable
    {
        Class1 s;

        public Form1()
        {
            InitializeComponent();

            s = new Class1();
        }

        
    }
}
