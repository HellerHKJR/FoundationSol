using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace BigFileRead
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

            Stopwatch st = new Stopwatch();
            
            if ( openFileDialog1.ShowDialog() == DialogResult.OK )
            {
                st.Start();
                textBox1.Text = "";
                int foundcount = 0;
                var lines = File.ReadLines(openFileDialog1.FileName, Encoding.Default);
                string zoneCaption = "";
                lines.Any(l =>
                {
                    if( l.StartsWith("Number of TCs") )
                    {
                        textBox1.Text += l + Environment.NewLine;
                        lblTC.Text = l.Split(',').Last();
                        foundcount++;
                    }
                    else if( l.StartsWith("Number of Statistics"))
                    {
                        textBox1.Text += l + Environment.NewLine;
                        lblStat.Text = l.Split(',').Last();
                        foundcount++;
                    }
                    else if( l.StartsWith("Barcode") )
                    {
                        zoneCaption = l;
                        textBox1.Text += l + Environment.NewLine;
                        foundcount++;
                    }

                    if (foundcount > 2) return true;
                    return false;
                });
                
                string resultData = lines.Last();
                textBox1.Text += resultData;
                st.Stop();

                lblElaps.Text = st.Elapsed.TotalMilliseconds + " milli-sec";


                int offsetCount = 5;
                int tcCount = Convert.ToInt32(lblTC.Text);
                int statCount = Convert.ToInt32(lblStat.Text);
                var captionSplit = zoneCaption.Split(',');
                var resultDataSplit = resultData.Split(',');
                string[] itemNames = new string[] { "data", "PWI", "Cpk"};

                for ( int iStat = 0; iStat < statCount; iStat++)
                {
                    //make name column
                    string captionHeader = captionSplit[offsetCount + iStat * tcCount * 3];

                    for ( int iTc = 0; iTc < tcCount; iTc++ )
                    {
                        for( int iItem =0; iItem < 3; iItem++)
                        {
                            string caption = $"{captionHeader} TC{2+iTc}-{itemNames[iItem]}";
                            string itemValue = $"{resultDataSplit[offsetCount + iStat * iTc * 3 + iItem]}";
                            dataGridView1.Rows.Add(caption, itemValue);
                        }

                    }
                }

            }
        }
    }
}
