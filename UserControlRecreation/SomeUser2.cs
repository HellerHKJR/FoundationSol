using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserControlRecreation
{
    public partial class SomeUser2 : UserControl
    {
        delegate void TestEvent();
        private event TestEvent OnTestEvent;

        public SomeUser2()
        {
            InitializeComponent();

            //Initialze();
        }

        public void Initialze()
        {
            CheckBox checkBox1 = new CheckBox();
            Button button1 = new Button();
            this.SuspendLayout();

            this.Controls.Clear();
            OnTestEvent = null;

            //
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(0, 0);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(80, 17);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(3, 23);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(this.button1_Click);
            //

            this.Controls.Add(button1);
            this.Controls.Add(checkBox1);
            this.ResumeLayout(false);
            this.PerformLayout();

            OnTestEvent += SomeUser2_OnTestEvent;
        }

        private void SomeUser2_OnTestEvent()
        {
            Console.WriteLine("{0} Event raise", DateTime.Now);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("{0} Button clicked", DateTime.Now);
            OnTestEvent?.Invoke();
        }
    }
}
