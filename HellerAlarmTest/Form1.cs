using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HellerAlarmTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Alarm_List list = new Alarm_List();

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i =0; i < 3; i++)
            list.Add(new HellerInterfaceAlarm(1000 + i, "test", true, 3, "infinit", 0));

            for (int i = 0; i < 3; i++)
            {
                HellerInterfaceAlarm f = list[i];
            }

        }
    }

    class HellerInterfaceAlarm
    {
        public int id;
        public string name;
        public bool enable;
        public int threshold;
        public string tracking;
        public int hits;

        public HellerInterfaceAlarm(int id, string name, bool enable, int threshold, string tracking, int hits)
        {
            this.id = id;
            this.name = name;
            this.enable = enable;
            this.threshold = threshold;
            this.tracking = tracking;
            this.hits = hits;
        }
    }

    class Alarm_List : KeyedCollection<int, HellerInterfaceAlarm>
    {
        protected override int GetKeyForItem(HellerInterfaceAlarm item)
        {
            return item.id;
        }
    }

}
