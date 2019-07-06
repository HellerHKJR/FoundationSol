using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reorder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<LotList> lotList = new List<LotList>();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LotList tmp = new LotList();
            tmp.LotID = "AB" + DateTime.Now.ToLongTimeString();
            lotList.Add(tmp);

            ValidateList();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string lotID = listBox1.SelectedItem as string;
            if( !string.IsNullOrWhiteSpace(lotID))
            {
                foreach( LotList tmp in lotList)
                {
                    if (tmp.LotID.Equals(lotID))
                    {
                        lotList.Remove(tmp);
                        break;
                    }
                }

                ValidateList();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            string lotID = listBox1.SelectedItem as string;
            if (!string.IsNullOrWhiteSpace(lotID))
            {
                LotList target = null;
                foreach (LotList tmp in lotList)
                {
                    if (tmp.LotID.Equals(lotID))
                    {
                        target = tmp;
                        break;
                    }
                }

                ReOrder(target, true);

                ValidateList();

                listBox1.SelectedItem = lotID;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            string lotID = listBox1.SelectedItem as string;
            if (!string.IsNullOrWhiteSpace(lotID))
            {
                LotList target = null;
                foreach (LotList tmp in lotList)
                {
                    if (tmp.LotID.Equals(lotID))
                    {
                        target = tmp;
                        break;
                    }
                }

                ReOrder(target, false);

                ValidateList();

                listBox1.SelectedItem = lotID;
            }
        }

        private void ValidateList()
        {
            List<string> tmp = new List<string>();
            lotList.All(a => { tmp.Add(a.LotID); return true; });
            listBox1.DataSource = tmp;
        }

        private void ReOrder(LotList lot, bool isUp)
        {
            int idxLot = -1;
            for (int i = 0; i < lotList.Count; i++)
            {
                if (lot.LotID.Equals(lotList[i].LotID)) { idxLot = i; break; }
            }

            if (isUp)
            {
                if (idxLot < 1) return;
                LotList tmpLot = lotList[idxLot - 1];
                lotList[idxLot - 1] = lotList[idxLot];
                lotList[idxLot] = tmpLot;
            }
            else
            {
                if (idxLot > lotList.Count - 2) return;
                LotList tmpLot = lotList[idxLot + 1];
                lotList[idxLot + 1] = lotList[idxLot];
                lotList[idxLot] = tmpLot;
            }

        }
    }

    public class LotList
    {
        public string LotID;
    }
}
