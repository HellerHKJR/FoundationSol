using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
                                                        int size, string filePath);

        StringBuilder sb = new StringBuilder(255);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //WritePrivateProfileString("TEST_SECTION", "Item1", "SSSSS", Environment.CurrentDirectory + "\\test.ini");
            GetPrivateProfileString("TEST_SECTION", "Item1", "", sb, sb.Capacity, Environment.CurrentDirectory + "\\test.ini");
            Console.WriteLine(sb.ToString());

            GetPrivateProfileString("TEST_SECTION", "Item2", "", sb, sb.Capacity, Environment.CurrentDirectory + "\\test.ini");
            Console.WriteLine(sb.ToString());
            GetPrivateProfileString("TEST_SECTION", "Item3", "", sb, sb.Capacity, Environment.CurrentDirectory + "\\test.ini");
            Console.WriteLine(sb.ToString());
            GetPrivateProfileString("TEST_SECTION", "Item4", "", sb, sb.Capacity, Environment.CurrentDirectory + "\\test.ini");

            Console.WriteLine(sb.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("a", "f", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show("a", "f", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            MessageBox.Show("a", "f", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
