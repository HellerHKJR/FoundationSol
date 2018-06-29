using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SECSDriverTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageData md = new MessageData();
            md.AddList(2);
            md.AddBinary(0);
            md.AddASCII("qwer");
            
            md.AddBoolean(true);
            md.AddJIS8("abcd");
            md.AddWORD(new char[] { 'a', 'b' });
            md.AddI1(-1);
            md.AddI1(-2);
            md.AddI1(-3);
            md.AddI2(1);
            md.AddI2(-1);
            md.AddI2(-2);
            md.AddI2(-3);

            md.AddI4(1);
            md.AddI8(1);

            md.AddU2(1);


            md.AddF4(1234.1234F);
            md.AddF8(1234.1234D);
        }

    }

    public class MessageData
    {
        public List<byte> msgData = new List<byte>();

        private byte GetNumberOfLengthByte(int length)
        {
            byte usingBytes = 1;
            if (length > 0xFF) usingBytes = 2;
            if (length > 0xFFFF) usingBytes = 3;
            if (length > 0xFFFFF)
            {
                Console.WriteLine("Exceeded max length {0}", 0xFFFFF);
                return 4;
            }

            return usingBytes;
        }

        private bool AddFormat(int length, ItemFormat itemFormat)
        {
            byte lengthByte = GetNumberOfLengthByte(length);
            if (lengthByte == 4) return false;

            byte ret = (byte)((byte)itemFormat << 2 | lengthByte);
            msgData.Add(ret);

            for (byte b = (byte)lengthByte; b > 0; b--)
            {
                byte retLen = (byte)(length >> (b-1) * 8 & 0xFF);
                msgData.Add(retLen);
            }

            return true;
        }

        public void AddList(int length)
        {
            AddFormat(2, ItemFormat.LIST);
        }

        public void AddBinary(byte datum)
        {
            AddFormat(1, ItemFormat.Binary);
            msgData.Add(datum);
        }

        public void AddASCII(string datum)
        {
            AddFormat(datum.Length, ItemFormat.ASCII);
            msgData.AddRange(Encoding.ASCII.GetBytes(datum));
        }

        public void AddBoolean(bool datum)
        {
            AddFormat(1, ItemFormat.Boolean);
            msgData.Add(Convert.ToByte(datum));
        }

        public void AddJIS8(string datum)
        {
            AddFormat(datum.Length, ItemFormat.JIS8);
            msgData.AddRange(Encoding.ASCII.GetBytes(datum));
        }

        public void AddWORD(char[] datum)
        {
            AddFormat(2, ItemFormat.WORD);
            msgData.AddRange(Encoding.ASCII.GetBytes(datum));
        }

        public void AddI1(sbyte datum)
        {
            AddFormat(1, ItemFormat.I1);
            msgData.Add((byte)datum);
        }

        public void AddI2(short datum)
        {
            AddFormat(sizeof(short), ItemFormat.I2);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        public void AddI4(int datum)
        {
            AddFormat(sizeof(int), ItemFormat.I4);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        public void AddI8(long datum)
        {
            AddFormat(sizeof(long), ItemFormat.I8);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        public void AddU1(byte datum)
        {
            AddFormat(1, ItemFormat.U1);
            msgData.Add(datum);
        }

        public void AddU2(ushort datum)
        {
            AddFormat(sizeof(ushort), ItemFormat.U2);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        public void AddU4(uint datum)
        {
            AddFormat(sizeof(uint), ItemFormat.U4);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        public void AddU8(ulong datum)
        {
            AddFormat(sizeof(ulong), ItemFormat.U8);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        public void AddF4(float datum)
        {
            AddFormat(sizeof(float), ItemFormat.F4);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        public void AddF8(double datum)
        {
            AddFormat(sizeof(double), ItemFormat.F8);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

    }

    public enum ItemFormat : byte
    {
        LIST = 0X00,
        Binary = 0x08,
        Boolean = 0x09,
        ASCII = 0x10,
        JIS8 = 0x11,
        WORD = 0x12,
        I8 = 0x18,
        I1 = 0x19,
        I2 = 0x1A,
        I4 = 0x1C,
        F8 = 0x20,
        F4 = 0x24,
        U8 = 0x28,
        U1 = 0x29,
        U2 = 0x2A,
        U4 = 0x2C,
    }
}
