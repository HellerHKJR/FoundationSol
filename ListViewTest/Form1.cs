using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListViewTest
{
    public partial class Form1 : Form
    {
        int lotNo = 0;
        public Form1()
        {
            InitializeComponent();
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            lotNo++;

            LotList.Add("LotNo" + lotNo, "LotNo" + lotNo, "ProdIDTest", "SegmentIDTest", "TestRecipe", "NormalTest", 100, 1);
            LotList tmp = LotList.GetLot("LotNo" + lotNo);

            ListViewItem itm = new ListViewItem(new string[] {
                tmp.LotID,
                tmp.LotQty + "",
                tmp.InputCount + "",
                tmp.OutputCount + "",
                tmp.JobType,
                tmp.RecipeID,
                tmp.ProductID,
                tmp.SegmentID,
                tmp.LotStatus.ToString()
            });

            itm.Name = "LotID" + lotNo;
            lstViewLot.Items.Add(itm);

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstViewLot.SelectedItems.Count > 0)
            {
                string deleteLot = lstViewLot.SelectedItems[0].Text;
                lstViewLot.Items.RemoveByKey(deleteLot);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //lstViewLot.Items
            MoveListViewItems(lstViewLot, true);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveListViewItems(lstViewLot, false);
        }

        private void MoveListViewItems(ListView sourceListView, bool isUp)
        {
            int dir = isUp ? -1 : 1;

            ListViewItem lvi = sourceListView.SelectedItems[0];
            if (lvi != null)
            {
                int index = lvi.Index + dir;
                if (index >= sourceListView.Items.Count)
                    index = sourceListView.Items.Count - 1;
                else if (index < 0)
                    index = 0;

                sourceListView.Items.RemoveAt(lvi.Index);
                sourceListView.Items.Insert(index, lvi);

                LotList.MoveLotSequence(lvi.Text, isUp);
            }

            Console.WriteLine("Lot Sequence");
            foreach ( LotList ll in LotList.lotList)
            {
                Console.WriteLine(ll.LotID);
            }
        }
    }


    public class LotList
    {
        //Oven의 특성상 Lot은 입력된 순서대로 진행되어야 한다.
        public enum eLotStatus
        {
            Initial, Lot_Validation_Result, Lot_Dispatch_Request, Ready_To_TrackIn, Lot_TrackIn_Result,
            //Ready_To_TrackIn : Lot_TrackIn_Result 에서 MES로 부터 Lot 정보가 오지 않으므로 임시 저장할 목적으로 사용함.                

        }

        private bool isBegun = false;
        public bool IsBegun { get => isBegun; }

        private bool isFinished = false;
        public bool IsFinished { get => isFinished; }

        private bool isManual = false;
        public bool IsManual { get => isManual; set => isManual = value; }

        public static List<LotList> lotList = new List<LotList>();
        public string LotID { get; set; }
        public string PriorityLotID { get; set; }
        public string ProductID { get; set; }
        public string SegmentID { get; set; }
        public string RecipeID { get; set; }
        public string JobType { get; set; }
        public int LotQty { get; set; }
        public int InputCount { get; set; }
        public int OutputCount { get; set; }
        public int OvenLane { get; set; }

        public eLotStatus LotStatus { get; set; }

        //NOT USE basketID, panelID 

        public static bool Add(LotList lot)
        {
            bool bAdd = true;
            foreach (LotList tmp in lotList)
            {
                if (tmp.LotID.Equals(lot.LotID))
                {
                    bAdd = false;
                    break;
                }
            }

            if (bAdd) lotList.Add(lot);

            return bAdd;
        }

        public static bool Add(string lotID, string priorityLotID, string productID, string segmentID, string recipeID, string jobType, int lotQty, int ovenLane = 1)
        {
            LotList lot = new LotList();
            lot.LotID = lotID;
            lot.PriorityLotID = priorityLotID;
            lot.ProductID = productID;
            lot.SegmentID = segmentID;
            lot.RecipeID = recipeID;
            lot.JobType = jobType;
            lot.LotQty = lotQty;
            lot.OvenLane = ovenLane;
            lot.OutputCount = 0;
            lot.InputCount = 0;
            lot.LotStatus = eLotStatus.Initial;

            return Add(lot);
        }

        public static bool Add(string lotID, string priorityLotID, string recipeID, string jobType, int lotQty, int ovenLane = 1)
        {
            LotList lot = new LotList();
            lot.LotID = lotID;
            lot.PriorityLotID = priorityLotID;
            lot.ProductID = "";
            lot.SegmentID = "";
            lot.RecipeID = recipeID;
            lot.JobType = jobType;
            lot.LotQty = lotQty;
            lot.OvenLane = ovenLane;
            lot.OutputCount = 0;
            lot.InputCount = 0;
            lot.LotStatus = eLotStatus.Initial;

            return Add(lot);
        }

        public static bool Add(string lotID, string priorityLotID, string jobType, int ovenLane = 1)
        {
            LotList lot = new LotList();
            lot.LotID = lotID;
            lot.PriorityLotID = priorityLotID;
            lot.ProductID = "";
            lot.SegmentID = "";
            lot.RecipeID = "";
            lot.JobType = jobType;
            lot.LotQty = 0;
            lot.OvenLane = ovenLane;
            lot.OutputCount = 0;
            lot.InputCount = 0;
            lot.LotStatus = eLotStatus.Initial;

            return Add(lot);
        }

        public static bool Remove(LotList lot)
        {
            return Remove(lot.LotID);
        }

        public static bool Remove(string lotId)
        {
            bool bRemoved = false;
            foreach (LotList tmp in lotList)
            {
                if (tmp.LotID.Equals(lotId))
                {
                    lotList.Remove(tmp);
                    bRemoved = true;
                    break;
                }
            }

            return bRemoved;
        }

        public static void PurgeLots()
        {
            lotList.Clear();
        }

        public static void MoveLotSequence(string lotId, bool isUp)
        {
            int dir = isUp ? -1 : 1;
            int lstIndex = lotList.FindIndex(l => l.LotID.Equals(lotId, StringComparison.CurrentCultureIgnoreCase));
            LotList lotToMove = lotList.Find(l => l.LotID.Equals(lotId, StringComparison.CurrentCultureIgnoreCase));
            int index = lstIndex + dir;

            if (index >= lotList.Count)
                index = lotList.Count - 1;
            else if (index < 0)
                index = 0;

            lotList.RemoveAt(lstIndex);
            lotList.Insert(index, lotToMove);
        }

        public static LotList GetLot(string lotId)
        {
            foreach (LotList tmp in lotList)
            {
                if (tmp.LotID.Equals(lotId))
                {
                    return tmp;
                }
            }
            return null;
        }

        public static LotList GetLotByStatus(eLotStatus st)
        {
            foreach (LotList tmp in lotList)
            {
                if (tmp.LotStatus == st)
                {
                    return tmp;
                }
            }
            return null;
        }

        // The first detected LotID regarding processing.
        public static LotList GetProcessingLot(ref bool isLotStart, ref bool isLotEnd, bool isInput = true, bool isChecking = true)
        {
            //Search Processing Lot
            isLotStart = false;
            isLotEnd = false;

            if (isChecking)
            {
                foreach (LotList tmp in lotList)
                {
                    if (tmp.isBegun && !tmp.isFinished)
                    {
                        //첫번째 진행중인 Lot 리턴
                        return tmp;
                    }
                }

                return null;
            }

            LotList rtnTmp = null;

            if (isInput)
            {
                //Search For Input Panel
                foreach (LotList tmp in lotList)
                {
                    if (tmp.LotStatus == eLotStatus.Lot_TrackIn_Result && !tmp.isFinished
                        && tmp.LotQty > tmp.InputCount)
                    {
                        rtnTmp = tmp;
                        isLotStart = !tmp.isBegun;
                        tmp.isBegun = true;
                        break;
                    }
                }
            }
            else
            {
                //Search For Output Panel
                foreach (LotList tmp in lotList)
                {
                    if (tmp.isBegun && !tmp.isFinished
                        && tmp.InputCount > tmp.OutputCount)
                    {
                        if (tmp.LotQty == tmp.OutputCount + 1) tmp.isFinished = true;

                        rtnTmp = tmp;
                        isLotEnd = tmp.isFinished;

                        break;
                    }
                }
            }

            return rtnTmp;
        }

        // Find Processable Lot for checking interlock SMEMA
        public static bool HasProcessableLot()
        {
            foreach (LotList tmp in lotList)
            {
                //대기열 Lot이 있는 경우
                if (tmp.LotStatus == eLotStatus.Lot_TrackIn_Result)
                {
                    // 시작되지 않았을 경우
                    if (!tmp.isBegun) return true;

                    //시작되었을 경우 input 수량이 Lot 수량보다 적은 경우 
                    if (tmp.isBegun && tmp.InputCount < tmp.LotQty) return true;
                }
            }

            return false;
        }

        // About SVID 39 Processing LotID List
        public static List<object> GetProcessingLotList()
        {
            List<string> ret = new List<string>();

            foreach (LotList tmp in lotList)
            {
                if (tmp.isBegun) ret.Add(tmp.LotID);
            }

            return ret.Select(s => (object)s).ToList();
        }

        public void CalcCount(bool isInput)
        {
            if (isInput)
                this.InputCount++;
            else
                this.OutputCount++;
        }


    }
}
