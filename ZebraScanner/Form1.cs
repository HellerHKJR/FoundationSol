using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreScanner;
using System.Threading;
using System.Xml;

namespace ZebraScanner
{
    public partial class Form1 : Form
    {
        CCoreScannerClass m_pCoreScanner;

        public Form1()
        {
            InitializeComponent();

            bool bInstanceCreate = false;
            while (!bInstanceCreate)
            {
                try
                {
                    m_pCoreScanner = new CCoreScannerClass();
                    bInstanceCreate = true;
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }

            if (!bInstanceCreate)
            {
                Console.WriteLine("Instance Create Failed");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Connect as USB IBM HID mode
            m_pCoreScanner.BarcodeEvent += new _ICoreScannerEvents_BarcodeEventEventHandler(M_pCoreScanner_BarcodeEvent);
            m_pCoreScanner.PNPEvent += new _ICoreScannerEvents_PNPEventEventHandler(M_pCoreScanner_PNPEvent);
            
            short[] m_arScannerTypes = new short[1];
            bool[] m_arSelectedTypes = new bool[1];
            for ( int i = 0; i < 1; i++)
            {
                m_arSelectedTypes[i] = false;
                m_arScannerTypes[i] = 0;
            }
            m_arSelectedTypes[0] = true;
            m_arScannerTypes[0] = 1;    //USB IBM HID
            int resultStatus = 1;   //Fail Status
            short m_nNumberOfTypes = 1;

            m_pCoreScanner.Open(0, m_arScannerTypes, m_nNumberOfTypes, out resultStatus);

            if(resultStatus != 0 )
            {
                Console.WriteLine("Scanner Open Failed");
                return;
            }

            int nEvents = 2;
            string strEvtIDs = "1,16"; //subscribe_barcode, subscribe_pnp
            string inXml = "<inArgs>" +
                                "<cmdArgs>" +
                                "<arg-int>" + nEvents + "</arg-int>" +
                                "<arg-int>" + strEvtIDs + "</arg-int>" +
                                "</cmdArgs>" +
                                "</inArgs>";

            int opCode = 1001; //Register events
            string outXml = "";
            resultStatus = 1;
            m_pCoreScanner.ExecCommand(opCode, ref inXml, out outXml, out resultStatus);

            //Call GetScanners Command again.
            short numOfScanners = 0;
            int[] scannerIdList = new int[1];
            resultStatus = 1;
            m_pCoreScanner.GetScanners(out numOfScanners, scannerIdList, out outXml, out resultStatus);

            resultStatus = 1;
            inXml = "<inArgs>" +
                                "<scannerID>" + 1 + "</scannerID>" +
                                "</inArgs>";

            opCode = 2019;// 5006;  //Trigger On
            outXml = "";
            m_pCoreScanner.ExecCommand(opCode, ref inXml, out outXml, out resultStatus);
            Console.WriteLine("result:{0}", resultStatus);
            Console.WriteLine(outXml);
        }

        private void M_pCoreScanner_PNPEvent(short eventType, ref string ppnpData)
        {

            if (eventType == 0) Console.WriteLine("Scanner is attached.");
            else
            {
                Console.WriteLine("Scanner is detached.");
            }

            Console.WriteLine(ppnpData);
        }
        
        private void M_pCoreScanner_BarcodeEvent(short eventType, ref string pscanData)
        {            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(pscanData);

            string strData = String.Empty;
            string barcode = xmlDoc.DocumentElement.GetElementsByTagName("datalabel").Item(0).InnerText;
            string[] numbers = barcode.Split(' ');

            foreach (string number in numbers)
            {
                if (String.IsNullOrEmpty(number)) break;
                strData += ((char)Convert.ToInt32(number, 16)).ToString();
            }

            Console.WriteLine("BarcodeEvent : {0}", strData);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            m_pCoreScanner.BarcodeEvent -= new _ICoreScannerEvents_BarcodeEventEventHandler(M_pCoreScanner_BarcodeEvent);
            m_pCoreScanner.PNPEvent -= new _ICoreScannerEvents_PNPEventEventHandler(M_pCoreScanner_PNPEvent);

            int resultStatus = 1;   //Fail Status
            int nEvents = 2;
            string strEvtIDs = "1,16"; //subscribe_barcode, subscribe_pnp
            string inXml = "<inArgs>" +
                                "<cmdArgs>" +
                                "<arg-int>" + nEvents + "</arg-int>" +
                                "<arg-int>" + strEvtIDs + "</arg-int>" +
                                "</cmdArgs>" +
                                "</inArgs>";

            int opCode = 1002;  //Unregister events
            string outXml = "";
            resultStatus = 1;
            m_pCoreScanner.ExecCommand(opCode, ref inXml, out outXml, out resultStatus);

            m_pCoreScanner.Close(0, out resultStatus);
        }

        private void btnTriggerOn_Click(object sender, EventArgs e)
        {
            int resultStatus = 1;   //Fail Status
            string inXml = "<inArgs>" +
                                "<scannerID>" + 1 + "</scannerID>" +
                                "</inArgs>";

            int opCode = 2011;  //Trigger On
            string outXml = "";
            m_pCoreScanner.ExecCommand(opCode, ref inXml, out outXml, out resultStatus);
        }

        private void btnTriggerOff_Click(object sender, EventArgs e)
        {
            int resultStatus = 1;   //Fail Status
            string inXml = "<inArgs>" +
                                "<scannerID>" + 1 + "</scannerID>" +
                                "</inArgs>";

            int opCode = 2012;  //Trigger Off
            string outXml = "";
            m_pCoreScanner.ExecCommand(opCode, ref inXml, out outXml, out resultStatus);
        }
    }
}
