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
            tcpConnectionTimer.Interval = 100;
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

                    //Thread thread = new Thread(new ThreadStart(SomethingFunction));
                    //thread.Start()
                    byte stationID = 1;

                    //System.Threading.Tasks.Task<bool[]> retReadInput = master.ReadInputsAsync(stationID, 0, 2);
                    //Thread.Sleep(300);
                    //System.Threading.Tasks.Task<bool[]> retReadCoils = master.ReadCoilsAsync(stationID, 2, 1);
                    //Thread.Sleep(300);
                    System.Threading.Tasks.Task<ushort[]> retReadHoldingRegisters = master.ReadHoldingRegistersAsync(stationID, 6, 6);
                    //Thread.Sleep(300);
                    //System.Threading.Tasks.Task<ushort[]> retReadInputRegisters = master.ReadInputRegistersAsync(stationID, 0, 2);
                    //Thread.Sleep(300);
                    //System.Threading.Tasks.Task<ushort[]> retReadWriteMultipleRegisters = master.ReadWriteMultipleRegistersAsync(stationID, 0, 2, 0, new ushort[] { 1, 2 });
                    //Thread.Sleep(300);
                    
                    //System.Threading.Tasks.Task reWriteSingleCoil = master.WriteMultipleCoilsAsync(stationID, 2, new bool[] { false });
                    //master.WriteMultipleCoilsAsync

                    try
                    {
                        //System.Threading.Tasks.Task.WaitAll(retReadInput, retReadCoils, retReadHoldingRegisters, retReadInputRegisters, retReadInputRegisters, retReadWriteMultipleRegisters);

                        System.Threading.Tasks.Task.WaitAll(retReadHoldingRegisters);
                        ushort[] ret = retReadHoldingRegisters.Result;
                        //bool[] ret = retReadInput.Result;

                        //if (ret[0]) lblDI0.BackColor = Color.Blue;
                        //else lblDI0.BackColor = Color.Red;

                        for( int s = 0; s < ret.Length; s++)
                        {
                            Console.WriteLine("{0}:{1:X2}", s, ret[s]);
                        }

                    }
                    catch (AggregateException aex)
                    {
                        Console.WriteLine("Timeout to Read somethings");
                        Console.WriteLine(aex);

                    }
                    catch (Exception) { }

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
                    master.Transport.ReadTimeout = 1500;
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
            serialPort.BaudRate = 4800;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.None;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            
            try
            {
                serialPort.Open();
                
                master = ModbusSerialMaster.CreateRtu(serialPort);
                master.Transport.Retries = 0;
                master.Transport.ReadTimeout = 500;
                Console.WriteLine("Connected to ModbusRTU");                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());                
            }

            return serialPort.IsOpen;
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
