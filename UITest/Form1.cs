using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UITest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(1, 2, 3);
            dataGridView1.Rows.Add(4, 5, 6);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    int i = 0;
                    i = i + 1;

                    
                }
            }
        }

        private void deleteStepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow d in dataGridView1.SelectedRows)
                dataGridView1.Rows.Remove(d);

            dataGridView1.ClearSelection();
        }
    }
}
