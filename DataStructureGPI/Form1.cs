using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace DataStructureGPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            BarcodeKeeping.MakeDataSet(4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageHeader mh = new MessageHeader(false, "yyyyMMddHHmmss");
            mh.McNo = "9999";
            mh.McName = "HellerCuringOven";
            mh.McStation = "99";

            MessagePayload root = new MessagePayload("HellerOven");
            MessagePayload pcbid = new MessagePayload("PCBID", "BARCODE1234");
            root.AppendItem(pcbid);
            MessagePayload laneNumber = new MessagePayload("LaneNumber", "9999");
            root.AppendItem(laneNumber);

            MessagePayload o2 = new MessagePayload("Oxygen");
            MessagePayload o2Concentration = new MessagePayload("O2Concentration", "100");

            MessagePayload o2Alarm1 = new MessagePayload("O2Alarm1");

            MessagePayload o2Alarm11 = new MessagePayload("O2Alarm11", "111");

            MessagePayload o2Alarm12 = new MessagePayload("O2Alarm12");
            MessagePayload o2Alarm121 = new MessagePayload("O2Alarm121", "111");
            MessagePayload o2Alarm122 = new MessagePayload("O2Alarm122", "111");
            o2Alarm12.AppendItem(o2Alarm121, o2Alarm122);

            MessagePayload o2Alarm13 = new MessagePayload("O2Alarm13", "333");
            o2Alarm1.AppendItem(o2Alarm11, o2Alarm12, o2Alarm13);

            MessagePayload o2Alarm2 = new MessagePayload("O2Alarm2", "400");
            o2.AppendItem(o2Concentration,o2Alarm1, o2Alarm2);

            root.AppendItem(o2);

            XmlMessage message = new XmlMessage("Seizo");
            string strMessage = message.CraeteXmlMessage(mh, root);
            string xmldoc = message.GetLog();
            Console.WriteLine(xmldoc);
            Console.WriteLine("===========");
            Console.WriteLine(strMessage);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            for (int i = 0; i < 4; i++)
            {
                // read barcode "12345"
                string retbarcode = "";
                bool isSuccessRead = BarcodeKeeping.ReadBarcode(i, string.Format($"Lane{i}_12345"), out retbarcode);
                Console.WriteLine("Board read {0} {1}", retbarcode, isSuccessRead ? "Success" : "Fail" );
            }

            for (int i = 0; i < 4; i++)
            {
                // enter board
                string retbarcode = "";
                bool isSuccessEnter = BarcodeKeeping.EnterBarcode(i, out retbarcode);

                Console.WriteLine("Board Enter {0} {1}", retbarcode, isSuccessEnter ? "Success" : "Fail");
            }

            for (int i = 0; i < 4; i++)
            {
                // read barcode "12345"
                string retbarcode = "";
                bool isSuccessRead = BarcodeKeeping.ReadBarcode(i, string.Format($"Lane{i}_12345"), out retbarcode);
                Console.WriteLine("Board read {0} {1}", retbarcode, isSuccessRead ? "Success" : "Fail");
            }

            for (int k = 0; k < 2; k++)
                for (int i = 0; i < 4; i++)
            {
                // enter board
                string retbarcode = "";
                bool isSuccessEnter = BarcodeKeeping.EnterBarcode(i, out retbarcode);

                Console.WriteLine("Board Enter {0} {1}", retbarcode, isSuccessEnter ? "Success" : "Fail");
            }

            for (int k = 0; k < 4; k++)
                for (int i = 0; i < 4; i++)
            {
                // exit board
                string retbarcode = "";
                bool isSuccessExit = BarcodeKeeping.ExitBarcode(i, out retbarcode);

                Console.WriteLine("Board Exit {0} {1}", retbarcode, isSuccessExit ? "Success" : "Fail");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int httpStatus = 103;
            string httpErrorString = "";
            string httpErrorParsing = Enum.GetName(typeof(HttpStatusCode), httpStatus);
            if (string.IsNullOrWhiteSpace(httpErrorParsing)) httpErrorParsing = "Undefined http error";
            httpErrorString = string.Format($"Http Error : ({httpStatus}) {httpErrorParsing}");

            Console.WriteLine(httpErrorString);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string tmp = @"C:\Oven\Recipe Files\1808.JOB";
            string ret = Path.GetFileNameWithoutExtension(tmp);

            Console.WriteLine(ret);
        }
    }

    class XmlMessage
    {
        private XmlDocument doc;
        private XmlNode node;

        public XmlMessage(string messageName)
        {
            doc = new XmlDocument();
            node = doc.AppendChild(doc.CreateElement("Body"));
            node = node.AppendChild(doc.CreateElement(messageName));
        }

        public string CraeteXmlMessage(MessageHeader mh, MessagePayload mi)
        {
            node.AppendChild(mh.GetMessage(doc));
            XmlNode payload = node.AppendChild(doc.CreateElement("HellerOven"));
            MakeInnerXmlItemElement(ref payload, mi);

            return doc.InnerXml;
        }

        private void MakeInnerXmlItemElement(ref XmlNode node, MessagePayload mi)
        {
            if (mi.Value is string) node.InnerText = mi.Value.ToString();
            else
            {
                //list
                foreach (MessagePayload eachMi in (List<MessagePayload>)mi.Value)
                {
                    XmlNode newNode = doc.CreateNode(XmlNodeType.Element,eachMi.Name, null);
                    if (eachMi.Value is string) newNode.InnerText = eachMi.Value.ToString();
                    else
                        MakeInnerXmlItemElement(ref newNode, eachMi);

                    node.AppendChild(newNode);
                }
            }

        }

        public string GetLog()
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            writer.Formatting = Formatting.Indented;
            doc.WriteTo(writer);
            writer.Flush();
            return sw.ToString();
        }
    }

    class MessageHeader
    {
        public string HeaderName { get; private set; } = "Common";
        public string McNo { get; set; } = "9999";
        public string McName { get; set; } = "HellerCuringOven";
        public string McStation { get; set; } = "99";
        public string time { get; private set; }

        public MessageHeader(bool isUtc, string timeFormat)
        {
            DateTime tmpTime = isUtc ? DateTime.UtcNow : DateTime.Now;
            tmpTime = new DateTime(2011, 05, 06, 07, 08, 09);
            time = tmpTime.ToString(timeFormat);
        }

        public XmlNode GetMessage(XmlDocument doc)
        {
            XmlNode newNode = null;
            XmlNode ret = doc.CreateNode(XmlNodeType.Element, "Common", null);

            newNode = doc.CreateNode(XmlNodeType.Element, "MCNo", null);
            newNode.InnerText = McNo;
            ret.AppendChild(newNode);

            newNode = doc.CreateNode(XmlNodeType.Element, "MCName", null);
            newNode.InnerText = McName;
            ret.AppendChild(newNode);

            newNode = doc.CreateNode(XmlNodeType.Element, "MCStation", null);
            newNode.InnerText = McStation;
            ret.AppendChild(newNode);

            newNode = doc.CreateNode(XmlNodeType.Element, "Time", null);
            newNode.InnerText = time;
            ret.AppendChild(newNode);

            return ret;
        }
    }

    class MessagePayload
    {
        private string name = "";
        private object value = null;

        public string Name { get => name; }
        public object Value { get => value; }

        public void AppendItem(params MessagePayload[] mi)
        {
            if (value == null) value = new List<MessagePayload>();
            mi.All(mmi => 
            {
                ((List<MessagePayload>)value).Add(mmi);
                return true;
            });            
        }

        public MessagePayload(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        public MessagePayload(string name)
        {
            this.name = name;
        }
    }

    class BarcodeKeeping
    {
        private static Dictionary<int, Queue<string>> barcodeList = new Dictionary<int, Queue<string>>();
        private static Dictionary<int, string> tmpKeepBarcode = new Dictionary<int, string>();
        private static object queueMon = new object();

        /// <summary>
        /// Create Data
        /// </summary>
        /// <param name="laneCount"></param>
        public static void MakeDataSet(int laneCount)
        {
            for(int i = 0; i < laneCount; i++)
            {
                Queue<string> que = new Queue<string>();
                barcodeList.Add(i, que);
                tmpKeepBarcode.Add(i, "");
            }
        }

        /// <summary>
        /// Read barcode
        /// </summary>
        /// <param name="lane"></param>
        /// <param name="barcode"></param>
        /// <returns>false if already have same barcode in specific lane. And generate new barcode.[Dupxxx]</returns>
        public static bool ReadBarcode(int lane, string barcode, out string retBarcode)
        {
            bool isTaken = false;
            bool ret = true;
            try
            {
                Monitor.TryEnter(queueMon, ref isTaken);
                Queue<string> que = (Queue<string>)barcodeList[lane];

                if (que.Contains(barcode))
                {
                    tmpKeepBarcode[lane] = string.Format("Dup:{0}:{1}", lane, DateTime.Now.Ticks);
                    ret = false;
                }
                else tmpKeepBarcode[lane] = barcode;

                retBarcode = tmpKeepBarcode[lane];
            }
            finally
            {
                if (isTaken) Monitor.Exit(queueMon);
            }

            return ret;
        }

        /// <summary>
        /// Enter board
        /// </summary>
        /// <param name="lane"></param>
        /// <param name="barcode"></param>
        /// <returns>false if barcode is empty. And generate new barcode.[Unknownxxx]</returns>
        public static bool EnterBarcode(int lane, out string barcode)
        {
            bool isTaken = false;
            bool ret = true;
            try
            {
                Monitor.TryEnter(queueMon, ref isTaken);

                //if it has not beed received barcode, make it Unknown.
                if (string.IsNullOrWhiteSpace(tmpKeepBarcode[lane]))
                {
                    tmpKeepBarcode[lane] = string.Format("Unknown:{0}:{1}", lane, DateTime.Now.Ticks);
                    ret = false;
                }

                Queue<string> que = (Queue<string>)barcodeList[lane];
                que.Enqueue(tmpKeepBarcode[lane]);
                barcode = tmpKeepBarcode[lane];
                tmpKeepBarcode[lane] = "";
                
            }
            finally
            {
                if (isTaken) Monitor.Exit(queueMon);
            }

            return ret;
        }

        /// <summary>
        /// Exit board
        /// </summary>
        /// <param name="lane"></param>
        /// <param name="barcode"></param>
        /// <returns>false if board didn't enter. And generate new barcode.[Unexpectxxx]</returns>
        public static bool ExitBarcode(int lane, out string barcode)
        {
            string retBarcode = "";

            bool isTaken = false;
            bool ret = true;
            try
            {
                Monitor.TryEnter(queueMon, ref isTaken);

                Queue<string> que = (Queue<string>)barcodeList[lane];
                if (que.Count < 1)
                {
                    retBarcode = string.Format("Unexpect:{0}:{1}", lane, DateTime.Now.Ticks);
                    ret = false;
                }
                else
                {
                    retBarcode = que.Dequeue();
                }
            }
            finally
            {
                if (isTaken) Monitor.Exit(queueMon);
            }
            barcode = retBarcode;

            return ret;
        }
    }
}
