using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ActUtlTypeLib;

namespace MelTest
{
    public partial class Form1 : Form
    {
        private ActUtlTypeLib.ActUtlTypeClass mel = null;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            mel = new ActUtlTypeClass();
            mel.ActLogicalStationNumber = int.Parse(txtConnection.Text);
            int iRet = mel.Open();

            Console.WriteLine("Result: " + iRet);
        }

        private int ReadWriteM(string[] deviceAddrs, ref bool[] readWriteMem, bool isWrite)
        {
            //check if same size of input.
            if (deviceAddrs.Length != readWriteMem.Length) return -1;

            string addrs = string.Join("\n", deviceAddrs);
            short[] readMem = new short[deviceAddrs.Length];

            int iRest = 0;

            if (isWrite)
            {
                for( int i = 0; i < deviceAddrs.Length; i++ )
                {
                    readMem[i] = (short)(readWriteMem[i] ? 1 : 0);
                }
                iRest = mel.WriteDeviceRandom2(addrs, deviceAddrs.Length, ref readMem[0]);
            }
            else
            {
                iRest = mel.ReadDeviceRandom2(addrs, deviceAddrs.Length, out readMem[0]);
                for (int i = 0; i < deviceAddrs.Length; i++)
                {
                    if (readMem[i] == 0) readWriteMem[i] = false;
                    else readWriteMem[i] = true;
                }
            }

            return iRest;
        }

        private int ReadWriteD(string[] deviceAddrs, ref short[] readWriteMem, bool isWrite)
        {
            //check if same size of input.
            if (deviceAddrs.Length != readWriteMem.Length) return -1;

            string addrs = string.Join("\n", deviceAddrs);
            
            int iRest = 0;

            if (isWrite)
            {
                iRest = mel.WriteDeviceRandom2(addrs, deviceAddrs.Length, ref readWriteMem[0]);
            }
            else
            {
                iRest = mel.ReadDeviceRandom2(addrs, deviceAddrs.Length, out readWriteMem[0]);
            }

            return iRest;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            int iRet = mel.Close();

            Console.WriteLine("Result: " + iRet);
        }

        private string strTmp;
        private void btnReadM_Click(object sender, EventArgs e)
        {
            //string strAddrM = txtAddrM.Text;
            string[] strAddrMs = txtAddrM.Lines;

            Console.WriteLine("Input Addr M: " + strAddrMs);

            bool[] retBools = new bool[strAddrMs.Length];


            int iRet = ReadWriteM(strAddrMs, ref retBools, false);
            Console.WriteLine("ReadWriteM Result: 0x{0:x8}", iRet);
            if( iRet == 0 )
            {
                string[] tmpStrs = new string[retBools.Length];
                for (int ii = 0; ii < retBools.Length; ii++)
                    tmpStrs[ii] = retBools[ii].ToString();
                txtDataM.Lines = tmpStrs;
            }
        }

        private void btnWriteM_Click(object sender, EventArgs e)
        {
            string[] strAddrMs = txtAddrM.Lines;

            Console.WriteLine("Input Addr M: " + strAddrMs);

            bool[] retBools = new bool[strAddrMs.Length];

            for( int i = 0; i < strAddrMs.Length; i++)
            {
                retBools[i] = bool.Parse(txtDataM.Lines[i]);
            }
            
            int iRet = ReadWriteM(strAddrMs, ref retBools, true);
            Console.WriteLine("ReadWriteM Result: 0x{0:x8}", iRet);
            if (iRet == 0)
            {
                string[] tmpStrs = new string[retBools.Length];
                for (int ii = 0; ii < retBools.Length; ii++)
                    tmpStrs[ii] = retBools[ii].ToString();
                txtDataM.Lines = tmpStrs;
            }
        }

        private void btnReadD_Click(object sender, EventArgs e)
        {
            string[] strAddrMs = txtAddrD.Lines;

            Console.WriteLine("Input Addr D: " + strAddrMs);

            short[] retShorts = new short[strAddrMs.Length];
            
            int iRet = ReadWriteD(strAddrMs, ref retShorts, false);
            Console.WriteLine("ReadWriteM Result: 0x{0:x8}", iRet);

            ////32bit int
            //if( iRet == 0)
            //{
            //    //little to big
            //    int tmpRet = (int)(retShorts[1] << 16 & 0xffff0000)
            //                | (retShorts[0] << 0 & 0x0000ffff);

            //    //big to little
            //    short[] tmpShorts = new short[2];
            //    tmpShorts[1] = (short)(tmpRet >> 16);
            //    tmpShorts[0] = (short)(tmpRet);                
            //}

            
            //Character
            if (iRet == 0)
            {
                string[] tmpStrs = new string[retShorts.Length];
                string kkk = "";
                for (int ii = 0; ii < retShorts.Length; ii++)
                {
                    string tmpChars;
                    char tmpw = (char)(retShorts[ii] >> 8 & 0xFF);
                    char tmp = (char)(retShorts[ii] & 0xFF);                    
                    tmpChars = string.Join("", tmp, tmpw);

                    kkk += tmpChars;
                }

                //txtDataD.Lines = tmpStrs;
                txtDataD.Text = kkk;
            }
            
        }

        private void btnWriteD_Click(object sender, EventArgs e)
        {
            string[] strAddrMs = txtAddrD.Lines;

            Console.WriteLine("Input Addr D: " + strAddrMs);

            short[] retShorts = new short[strAddrMs.Length];

            for (int i = 0; i < strAddrMs.Length; i++)
            {
                retShorts[i] = short.Parse(txtDataD.Lines[i]);
            }
            
            int iRet = ReadWriteD(strAddrMs, ref retShorts, true);
            Console.WriteLine("ReadWriteM Result: 0x{0:x8}", iRet);
            if (iRet == 0)
            {
                string[] tmpStrs = new string[retShorts.Length];
                for (int ii = 0; ii < retShorts.Length; ii++)
                    tmpStrs[ii] = retShorts[ii].ToString();
                txtDataD.Lines = tmpStrs;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //0run, 1stop, 2pause
            mel.SetCpuStatus(1);
            //mel.setde

            ActSupportMsgLib.ActMLSupportMsgClass kk = new ActSupportMsgLib.ActMLSupportMsgClass();
            //kk.GetErrorMessage()
        }
    }
}
