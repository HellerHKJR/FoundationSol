using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using Modbus.Device;
using Modbus.Utility;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;


namespace ModbusTest
{
    public partial class Form1 : Form
    {

        [DllImport("WININET", CharSet = CharSet.Auto)]
        static extern bool InternetGetConnectedState(ref InternetConnectionState lpdwFlags, int dwReserved);
        enum InternetConnectionState : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        System.Windows.Forms.Timer tcpConnectionTimer;
        TcpClient tcpClient;
        SerialPort serialPort;
        ModbusMaster master;
        string ipAddress = "10.1.0.1";
        int tcpPort = 502;
        DateTime dtDisconnect = new DateTime();
        DateTime dtNow = new DateTime();
        bool NetworkIsOk = false;
        
        private void ModbusTCPStart()
        {
            NetworkIsOk = ConnectModbusTCP();

            tcpConnectionTimer = new System.Windows.Forms.Timer();
            tcpConnectionTimer.Interval = 100;
            tcpConnectionTimer.Tick += TcpConnectionTimer_Tick;
            tcpConnectionTimer.Enabled = true;
        }

        private void ModbusSerialStart()
        {
            NetworkIsOk = ConnectModbusSerial();

            tcpConnectionTimer = new System.Windows.Forms.Timer();
            tcpConnectionTimer.Interval = 500;
            tcpConnectionTimer.Tick += TcpConnectionTimer_Tick;
            tcpConnectionTimer.Enabled = true;
        }

        

        private void TcpConnectionTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (NetworkIsOk)
                {
                    #region Master to Slave

                    Console.WriteLine("Try to Read somethings : {0}", DateTime.Now.ToString("HH:mm:ss.fff"));

                    //Thread thread = new Thread(new ThreadStart(SomethingFunction));
                    //thread.Start()
                    byte stationID = 1;

                    ////System.Threading.Tasks.Task<bool[]> retReadInput = master.ReadInputsAsync(stationID, 0, 2);
                    ////System.Threading.Tasks.Task<bool[]> retReadCoils = master.ReadCoilsAsync(stationID, 2, 1);
                    ////System.Threading.Tasks.Task<ushort[]> retReadHoldingRegisters = master.ReadHoldingRegistersAsync(stationID, 6, 6);

                    //////
                    //삼성전기 쿤산 GIMAC 의 읽는 부분
                    ////// 30001 ~ 30026, 실제 사용부분 : 30001(Avg Vol, 2byte), 30003(Avg Cur, 2byte),  30023(Factor, 2 byte), 30025 (Total Active Power, 2 byte)
                    System.Threading.Tasks.Task<ushort[]> retReadInputRegisters = master.ReadInputRegistersAsync(stationID, 0, 26);
                    // InputRegistersAsync

                    ////System.Threading.Tasks.Task<ushort[]> retReadWriteMultipleRegisters = master.ReadWriteMultipleRegistersAsync(stationID, 0, 2, 0, new ushort[] { 1, 2 });

                    //System.Threading.Tasks.Task reWriteSingleCoil = master.WriteMultipleCoilsAsync(stationID, 2, new bool[] { false });
                    //master.WriteMultipleCoilsAsync

                    try
                    {
                        //bool waitSuccess = System.Threading.Tasks.Task.WaitAll(new System.Threading.Tasks.Task[] { retReadInput, retReadCoils, retReadHoldingRegisters, retReadInputRegisters, retReadWriteMultipleRegisters }, 3000);
                        
                        bool waitSuccess = retReadInputRegisters.Wait(200);
                        

                        if (!waitSuccess)
                        {
                            Console.WriteLine("Timeout to Read somethings");

                            return;
                        }

                        //System.Threading.Tasks.Task.WaitAll(retReadHoldingRegisters);
                        ////bool[] ret1 = retReadInput.Result;
                        ////for (int s = 0; s < ret1.Length; s++)
                        ////{
                        ////    Console.WriteLine("ReadInput {0}:{1}", s, ret1[s]);
                        ////}

                        ////bool[] ret2 = retReadCoils.Result;
                        ////for (int s = 0; s < ret2.Length; s++)
                        ////{
                        ////    Console.WriteLine("ReadCoils {0}:{1}", s, ret2[s]);
                        ////}

                        ////ushort[] ret3 = retReadHoldingRegisters.Result;
                        ////for (int s = 0; s < ret3.Length; s++)
                        ////{
                        ////    Console.WriteLine("ReadHoldingRegisters {0}:{1:X2}", s, ret3[s]);
                        ////}

                        ushort[] ret4 = retReadInputRegisters.Result;
                        if(ret4.Length == 26)
                        {
                            //AVG Volt
                            float value = ModbusUtility.GetSingle(ret4[1], ret4[0]);
                            txtVolt.Text = value.ToString("F3");                            
                            Console.WriteLine("Voltage {0}", value);
                            //AVG Curr
                            value = ModbusUtility.GetSingle(ret4[3], ret4[2]);
                            txtCurr.Text = value.ToString("F3");
                            Console.WriteLine("Current {0}", value);
                            //AVG Fact
                            value = ModbusUtility.GetSingle(ret4[23], ret4[22]);
                            txtFact.Text = value.ToString("F3");
                            Console.WriteLine("Power Factor {0}", value);
                            //AVG Powe
                            value = ModbusUtility.GetSingle(ret4[25], ret4[24]);
                            txtPowe.Text = value.ToString("F3");
                            Console.WriteLine("Total Active Power {0}", value);
                        }
                        ////for (int s = 0; s < ret4.Length; s++)
                        ////{
                        ////    Console.WriteLine("ReadInputRegisters {0}:{1:X2}", s, ret4[s]);
                        ////}

                        ////ushort[] ret5 = retReadWriteMultipleRegisters.Result;
                        ////for (int s = 0; s < ret5.Length; s++)
                        ////{
                        ////    Console.WriteLine("ReadWriteMultipleRegisters {0}:{1:X2}", s, ret5[s]);
                        ////}

                        //if (ret[0]) lblDI0.BackColor = Color.Blue;
                        //else lblDI0.BackColor = Color.Red;

                    }
                    catch (AggregateException aex)
                    {
                        Console.WriteLine("Exception to Read somethings : {0}", DateTime.Now.ToString("HH:mm:ss.fff"));
                        Console.WriteLine(aex);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    #endregion
                }
                else
                {
                    //retry connecting
                    dtNow = DateTime.Now;
                    if ((dtNow - dtDisconnect) > TimeSpan.FromSeconds(2))
                    {
                        Console.WriteLine(DateTime.Now.ToString() + ":Start connecting");
                        //NetworkIsOk = ConnectModbusTCP();

                        NetworkIsOk = ConnectModbusSerial();
                        if (!NetworkIsOk)
                        {
                            Console.WriteLine(DateTime.Now.ToString() + ":Connecting fail. Wait for retry");
                            dtDisconnect = DateTime.Now;
                        }
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToString() + ":Wait for retry connecting");
                    }
                }
            }            
            catch (Exception ex)
            {
                //if (ex.Source.Equals("System"))
                {
                    //retry connecting
                    NetworkIsOk = false;
                    Console.WriteLine(ex.Message);
                    dtDisconnect = DateTime.Now;
                }
            }
        }

        private void SomethingFunction()
        {
            throw new NotImplementedException();
        }

        private bool ConnectModbusTCP()
        {
            if (master != null) master.Dispose();
            if (tcpClient != null) tcpClient.Close();
            if (CheckInternet() )
            {
                try
                {
                    tcpClient = new TcpClient();
                    IAsyncResult asyncResult = tcpClient.BeginConnect(ipAddress, tcpPort, null, null);
                    asyncResult.AsyncWaitHandle.WaitOne(3000, true);
                    if (!asyncResult.IsCompleted)
                    {
                        tcpClient.Close();
                        Console.WriteLine("Cannot connect to ModbusTCP");
                        return false;
                    }

                    master = ModbusIpMaster.CreateIp(tcpClient);
                    master.Transport.Retries = 0;
                    master.Transport.ReadTimeout = 2000;
                    Console.WriteLine("Connected to ModbusTCP");
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        private bool ConnectModbusSerial()
        {
            if (master != null) master.Dispose();
            if (serialPort != null) serialPort.Close();
            serialPort = new SerialPort();
            serialPort.PortName = "COM6";
            serialPort.BaudRate = 38400;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.None;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            
            try
            {
                serialPort.Open();
                
                master = ModbusSerialMaster.CreateRtu(serialPort);
                master.Transport.Retries = 0;
                master.Transport.ReadTimeout = 300;
                Console.WriteLine("Connected to ModbusRTU");                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());                
            }

            return serialPort.IsOpen;
        }

        private void MainDispose()
        {
            if( serialPort != null && serialPort.IsOpen )
            {
                serialPort.Close();
            }
        }

        private bool CheckInternet()
        {
            InternetConnectionState flag = InternetConnectionState.INTERNET_CONNECTION_LAN;
            return InternetGetConnectedState(ref flag, 0);
        }


        public Form1()
        {
            InitializeComponent();

            //ModbusTCPStart();
            ModbusSerialStart();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //TcpClient tcpClient = new TcpClient("10.1.0.1", 502);
            //ModbusIpMaster mm = ModbusIpMaster.CreateIp(tcpClient);

            //byte a = 0;
            //ushort b = 0;
            //ushort c = 0;

            //mm.ReadCoilsAsync(a, b, c);
        }
    }
}
