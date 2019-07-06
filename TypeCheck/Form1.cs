using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeCheck
{
    public enum testEnum : int { a, b, c}
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e1)
        {
            float a = 12;
            int b = 11;
            string c = "10";
            bool d = true;
            bool e = false;
            List<string> f = new List<string>();
            List<int> g = new List<int>();
            testEnum h = testEnum.b;
            object[] test = new object[]{ a, b, c, d, e, f, g, h };

            for( int i = 0; i < test.Length; i++ )
            {
                if (test[i] is float) Console.WriteLine("this is float {0}", test[i]);
                else if (test[i] is int) Console.WriteLine("this is int {0}", test[i]);
                else if (test[i] is string) Console.WriteLine("this is string {0}", test[i]);
                else if (test[i] is bool) Console.WriteLine("this is bool {0}", test[i]);                
                else if (test[i].GetType() == typeof(List<object>)) Console.WriteLine("this is List<string>", test[i].GetType());
                else if (test[i] is Enum ) Console.WriteLine("this is enum {0}", (int)h + "");
            }

            if (test[3].Equals(test[4])) Console.WriteLine("they are euqual");
            else Console.WriteLine("they are different");


        }
    }
}
