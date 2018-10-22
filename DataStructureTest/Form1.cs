using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace DataStructureTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch st = Stopwatch.StartNew();

            for (int i = 0; i < 1; i++)
            {
                st.Restart();
                MessageData md = new MessageData();
                MessageItem mi;

                mi = new MessageItem("", 3);
                md.AppendItem(mi);
                {
                    mi = new MessageItem(HComEnums.ItemFormat.I4, "DATAID", (int)2);
                    md.AppendItem(mi);
                    mi = new MessageItem(HComEnums.ItemFormat.I4, "CEID", (int)1920);
                    md.AppendItem(mi);
                    mi = new MessageItem("RPTIDCOUNT", 2);
                    md.AppendItem(mi);
                    {
                        mi = new MessageItem("RPTIDLIST", 2);
                        md.AppendItem(mi);
                        {
                            mi = new MessageItem(HComEnums.ItemFormat.U2, "RPTID", (ushort)222);
                            md.AppendItem(mi);
                            mi = new MessageItem("VCOUNT", 2);
                            md.AppendItem(mi);
                            {
                                mi = new MessageItem(HComEnums.ItemFormat.A, "VCOUNT", "0");
                                md.AppendItem(mi);
                                mi = new MessageItem(HComEnums.ItemFormat.A, "VCOUNT", "op1");
                                md.AppendItem(mi);
                            }
                        }

                        mi = new MessageItem("RPTIDLIST", 2);
                        md.AppendItem(mi);
                        {
                            mi = new MessageItem(HComEnums.ItemFormat.U2, "RPTID", (ushort)333);
                            md.AppendItem(mi);
                            mi = new MessageItem("VCOUNT", 2);
                            md.AppendItem(mi);
                            {
                                mi = new MessageItem(HComEnums.ItemFormat.A, "VCOUNT", "abcd");
                                md.AppendItem(mi);
                                mi = new MessageItem(HComEnums.ItemFormat.A, "VCOUNT", "op2");
                                md.AppendItem(mi);
                            }
                        }
                    }
                }

                XmlTextWriter writer = new XmlTextWriter(Console.Out);
                writer.Formatting = Formatting.None;
                XmlDocument xmldoc = md.GetLog(writer);
                Console.WriteLine();
                Console.WriteLine(xmldoc.InnerXml);
                Console.WriteLine("Get Byte to XML------");
                Console.WriteLine(md.GetData());

                MessageData mdRecv = new MessageData();
                mdRecv.SetData(md.GetData());
                mdRecv.GetLog(writer);
                Console.WriteLine();

                st.Stop();
                Console.WriteLine("Time Elapsed {0:000}:{1:000}", i, st.ElapsedMilliseconds);
            }
        }
    }

    public class MessageData
    {
        private List<byte> msgData = new List<byte>();

        private XmlDocument doc;
        private XmlNode lastAddedNode;

        public MessageData()
        {
            doc = new XmlDocument();
            lastAddedNode = null;
        }

        public byte[] GetData()
        {
            return msgData.ToArray();
        }

        public XmlDocument GetLog(XmlTextWriter writer)
        {
            doc.WriteTo(writer);
            
            writer.Flush();

            return doc;
        }

        public XmlDocument GetDoc()
        {
            return doc;
        }

        public void SetData(byte[] datum)
        {
            msgData.AddRange(datum);
            
            //Reverse
            //Get MessageItem Information from byteData
            while( datum.Length > 0 )
            {
                AddFormat(ref datum);
            }

        }

        public void AppendItem(MessageItem mi)
        {

            //build byte array for transferring
            switch (mi.type)
            {
                case HComEnums.ItemFormat.A: AddASCII((string)mi.value); break;
                case HComEnums.ItemFormat.B: AddBinary((byte)mi.value); break;
                case HComEnums.ItemFormat.Bool: AddBoolean((bool)mi.value); break;
                case HComEnums.ItemFormat.F4: AddF4((float)mi.value); break;
                case HComEnums.ItemFormat.F8: AddF8((double)mi.value); break;
                case HComEnums.ItemFormat.I1: AddI1((sbyte)mi.value); break;
                case HComEnums.ItemFormat.I2: AddI2((short)mi.value); break;
                case HComEnums.ItemFormat.I4: AddI4((int)mi.value); break;
                case HComEnums.ItemFormat.I8: AddI8((long)mi.value); break;
                case HComEnums.ItemFormat.JIS: AddJIS8((string)mi.value); break;
                case HComEnums.ItemFormat.L: AddList(mi.len); break;
                case HComEnums.ItemFormat.U1: AddU1((byte)mi.value); break;
                case HComEnums.ItemFormat.U2: AddU2((ushort)mi.value); break;
                case HComEnums.ItemFormat.U4: AddU4((uint)mi.value); break;
                case HComEnums.ItemFormat.U8: AddU8((ulong)mi.value); break;
                case HComEnums.ItemFormat.W: AddWORD((ushort)mi.value); break;
            }

            AppendXML(mi);
        }

        private void AppendXML(MessageItem mi)
        { 
            //build data to xml to used for log and process logic.

            try
            {
                XmlElement newNode = doc.CreateElement(mi.type.ToString());
                ////Alias
                if (!string.IsNullOrWhiteSpace(mi.name))
                {
                    XmlAttribute newAttributeAlias = doc.CreateAttribute("n");
                    newAttributeAlias.Value = mi.name;
                    newNode.Attributes.Append(newAttributeAlias);
                }

                ////Length
                XmlAttribute newAttributeLen = doc.CreateAttribute("l");                
                switch (mi.type)
                {
                    case HComEnums.ItemFormat.A: newAttributeLen.Value = ((string)mi.value).Length.ToString(); newNode.InnerText = (string)mi.value; break;
                    case HComEnums.ItemFormat.B: newAttributeLen.Value = sizeof(byte).ToString(); newNode.InnerText = ((byte)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.Bool: newAttributeLen.Value = sizeof(bool).ToString(); newNode.InnerText = ((bool)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.F4: newAttributeLen.Value = sizeof(float).ToString(); newNode.InnerText = ((float)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.F8: newAttributeLen.Value = sizeof(double).ToString(); newNode.InnerText = ((double)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.I1: newAttributeLen.Value = sizeof(sbyte).ToString(); newNode.InnerText = ((sbyte)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.I2: newAttributeLen.Value = sizeof(short).ToString(); newNode.InnerText = ((short)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.I4: newAttributeLen.Value = sizeof(int).ToString(); newNode.InnerText = ((int)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.I8: newAttributeLen.Value = sizeof(long).ToString(); newNode.InnerText = ((long)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.JIS: newAttributeLen.Value = ((string)mi.value).Length.ToString(); newNode.InnerText = (string)mi.value; break;
                    case HComEnums.ItemFormat.L: newAttributeLen.Value = mi.len.ToString(); break;
                    case HComEnums.ItemFormat.U1: newAttributeLen.Value = sizeof(byte).ToString(); newNode.InnerText = ((byte)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.U2: newAttributeLen.Value = sizeof(ushort).ToString(); newNode.InnerText = ((ushort)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.U4: newAttributeLen.Value = sizeof(uint).ToString(); newNode.InnerText = ((uint)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.U8: newAttributeLen.Value = sizeof(ulong).ToString(); newNode.InnerText = ((ulong)mi.value).ToString(); break;
                    case HComEnums.ItemFormat.W: newAttributeLen.Value = sizeof(ushort).ToString(); newNode.InnerText = ((ushort)mi.value).ToString(); break;
                }                
                newNode.Attributes.Append(newAttributeLen);

                XmlNode previousNode = FindPreviousList(lastAddedNode);
                if (previousNode != null)
                {
                    previousNode.AppendChild(newNode);
                }
                else
                {
                    if( doc.HasChildNodes ) throw new Exception("List Unmatched.");
                    doc.AppendChild(newNode);
                }

                lastAddedNode = newNode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private XmlNode FindPreviousList(XmlNode tmpNode)
        {
            XmlNode ret = null;

            if( tmpNode != null )
            {
                if (tmpNode.Name.Equals(nameof(HComEnums.ItemFormat.L)) &&
                    int.Parse(tmpNode.Attributes.GetNamedItem("l").Value) > tmpNode.ChildNodes.Count)
                    ret = tmpNode;
                else
                    ret = FindPreviousList(tmpNode.ParentNode);
            }
            else
            {
                ret = tmpNode;
            }
            
            return ret;
        }

        private HComEnums.NumberOfLengthByte GetNumberOfLengthByte(int length)
        {
            HComEnums.NumberOfLengthByte usingBytes = HComEnums.NumberOfLengthByte.Len1;
            if (length > 0xFF) usingBytes = HComEnums.NumberOfLengthByte.Len2;
            if (length > 0xFFFF) usingBytes = HComEnums.NumberOfLengthByte.Len3;
            if (length > 0xFFFFF)
            {
                Console.WriteLine("Exceeded max length {0}", 0xFFFFF);
                return HComEnums.NumberOfLengthByte.Overbound;
            }

            return usingBytes;

        }

        private bool AddFormat(int length, HComEnums.ItemFormat itemFormat)
        {
            HComEnums.NumberOfLengthByte lengthByte = GetNumberOfLengthByte(length);
            if (lengthByte == HComEnums.NumberOfLengthByte.Overbound) return false;

            byte ret = (byte)((byte)itemFormat << 2 | (byte)lengthByte);
            msgData.Add(ret);

            for (byte b = (byte)lengthByte; b > 0; b--)
            {
                byte retLen = (byte)(length >> (b - 1) * 8 & 0xFF);
                msgData.Add(retLen);
            }

            return true;
        }

        private void AddFormat(ref byte[] datum)
        {
            //Get Format and Length part's Length
            byte byteFormat = datum[0];
            int iLenAddrLength = byteFormat & 0x3;
            HComEnums.ItemFormat itemFormat = (HComEnums.ItemFormat)(byteFormat >> 2 & 0x3F);
            datum = datum.Skip(1).ToArray();

            //Get Data Length
            byte[] byteLength = new byte[4];
            for( int i = 0; i < iLenAddrLength; i++ )
            {
                byteLength[i] = datum[i];
            }
            datum = datum.Skip(iLenAddrLength).ToArray();
            int iLength = BitConverter.ToInt32(byteLength, 0);

            //Get Data
            MessageItem mi = null;

            if (itemFormat == HComEnums.ItemFormat.L)
                mi = new MessageItem("", iLength);
            else
                mi = MessageItem.MakeMessageItem(itemFormat, ref datum, iLength);

            AppendXML(mi);
            
        }

        private void AddList(int length)
        {
            AddFormat(length, HComEnums.ItemFormat.L);

        }

        private void AddBinary(byte datum)
        {
            AddFormat(1, HComEnums.ItemFormat.B);
            msgData.Add(datum);
        }

        private void AddBoolean(bool datum)
        {
            AddFormat(1, HComEnums.ItemFormat.Bool);
            msgData.Add(Convert.ToByte(datum));
        }

        private void AddASCII(string datum)
        {
            AddFormat(datum.Length, HComEnums.ItemFormat.A);
            msgData.AddRange(Encoding.ASCII.GetBytes(datum));
        }
        
        public void AddJIS8(string datum)
        {
            AddFormat(datum.Length, HComEnums.ItemFormat.JIS);
            msgData.AddRange(Encoding.ASCII.GetBytes(datum));
        }

        private void AddWORD(ushort datum)
        {
            AddFormat(2, HComEnums.ItemFormat.W);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        private void AddI8(long datum)
        {
            AddFormat(sizeof(long), HComEnums.ItemFormat.I8);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        private void AddI1(sbyte datum)
        {
            AddFormat(1, HComEnums.ItemFormat.I1);
            msgData.Add((byte)datum);
        }

        private void AddI2(short datum)
        {
            AddFormat(sizeof(short), HComEnums.ItemFormat.I2);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        private void AddI4(int datum)
        {
            AddFormat(sizeof(int), HComEnums.ItemFormat.I4);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        private void AddF8(double datum)
        {
            AddFormat(sizeof(double), HComEnums.ItemFormat.F8);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        private void AddF4(float datum)
        {
            AddFormat(sizeof(float), HComEnums.ItemFormat.F4);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        private void AddU8(ulong datum)
        {
            AddFormat(sizeof(ulong), HComEnums.ItemFormat.U8);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        private void AddU1(byte datum)
        {
            AddFormat(1, HComEnums.ItemFormat.U1);
            msgData.Add(datum);
        }

        private void AddU2(ushort datum)
        {
            AddFormat(sizeof(ushort), HComEnums.ItemFormat.U2);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

        private void AddU4(uint datum)
        {
            AddFormat(sizeof(uint), HComEnums.ItemFormat.U4);
            msgData.AddRange(BitConverter.GetBytes(datum).Reverse());
        }

    }

    public class MessageItem
    {
        public HComEnums.ItemFormat type;
        public int len;
        public string name;
        public object value;

        public MessageItem()
        {
        }
        //For All type but List
        public MessageItem(HComEnums.ItemFormat type, string name, object value)
        {
            this.type = type;
            this.name = name;
            this.value = value;
        }

        //For All type but List. From Remote
        public static MessageItem MakeMessageItem(HComEnums.ItemFormat type, ref byte[] bytes, int len)
        {
            MessageItem mi = new MessageItem();
            mi.type = type;
            mi.name = "";
            object formatValue = null;
            switch(type)
            {
                case HComEnums.ItemFormat.A: formatValue = Encoding.ASCII.GetString(bytes, 0, len); mi.len = len;  break;
                case HComEnums.ItemFormat.B: formatValue = bytes[0]; mi.len = 1;  break;
                case HComEnums.ItemFormat.Bool: formatValue = bytes[0] > 0 ? true : false; mi.len = 1; break;
                case HComEnums.ItemFormat.F4: formatValue = BitConverter.ToSingle(bytes.Take(4).Reverse().ToArray(), 0); mi.len = 4; break;
                case HComEnums.ItemFormat.F8: formatValue = BitConverter.ToDouble(bytes.Take(8).Reverse().ToArray(), 0); mi.len = 8; break;
                case HComEnums.ItemFormat.I1: formatValue = (sbyte)BitConverter.ToChar(bytes, 0); mi.len = 1; break;
                case HComEnums.ItemFormat.I2: formatValue = BitConverter.ToInt16(bytes.Take(2).Reverse().ToArray(), 0); mi.len = 2; break;
                case HComEnums.ItemFormat.I4: formatValue = BitConverter.ToInt32(bytes.Take(4).Reverse().ToArray(), 0); mi.len = 4; break;
                case HComEnums.ItemFormat.I8: formatValue = BitConverter.ToInt64(bytes.Take(8).Reverse().ToArray(), 0); mi.len = 8; break;
                case HComEnums.ItemFormat.JIS: formatValue = Encoding.ASCII.GetString(bytes, 0, len); mi.len = len; break;
                case HComEnums.ItemFormat.L: break;
                case HComEnums.ItemFormat.U1: formatValue = bytes[0]; mi.len = 1; break;
                case HComEnums.ItemFormat.U2: formatValue = BitConverter.ToUInt16(bytes.Take(2).Reverse().ToArray(), 0); mi.len = 2; break;
                case HComEnums.ItemFormat.U4: formatValue = BitConverter.ToUInt32(bytes.Take(4).Reverse().ToArray(), 0); mi.len = 4; break;
                case HComEnums.ItemFormat.U8: formatValue = BitConverter.ToUInt64(bytes.Take(8).Reverse().ToArray(), 0); mi.len = 8; break;
                case HComEnums.ItemFormat.W: formatValue = BitConverter.ToUInt16(bytes.Take(2).Reverse().ToArray(), 0); mi.len = 2; break;
            }
            mi.value = formatValue;

            bytes = bytes.Skip(mi.len).ToArray();

            return mi;
        }

        //For List Type
        public MessageItem(string name, int len)
        {
            this.type = HComEnums.ItemFormat.L;
            this.name = name;
            this.len = len;
        }

        
    }




    public class HComEnums
    {
        public enum NumberOfLengthByte : byte
        {
            Len1 = 0x01,
            Len2 = 0x02,
            Len3 = 0x03,
            Overbound

        }
        public enum ItemFormat : byte
        {
            L = 0X00,
            B = 0x08,
            Bool = 0x09,
            A = 0x10,
            JIS = 0x11,
            W = 0x12,
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
}
