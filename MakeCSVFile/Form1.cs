using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MakeCSVFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int idx = 0; idx < 100; idx++)
            {
                List<string> appendContent = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    appendContent.Add(string.Format($"Column{idx * 100 + i + 1}"));
                }

                AddContent(appendContent, ",", Environment.CurrentDirectory + @"\test.csv");
            }
        }

        private void AddContent(List<string> content, string delemeter, string filename)
        {
            string newLine = "";
            content.ForEach(c => newLine += string.Format($"{c}{delemeter}"));
            newLine = newLine.TrimEnd(',') + Environment.NewLine;

            File.AppendAllText(filename, newLine);            
        }

    }
}
