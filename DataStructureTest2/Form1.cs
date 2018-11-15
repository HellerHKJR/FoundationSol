using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataStructureTest2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageSet msg = new MessageSet();
            msg.MakeSendMessage("TEST");

            MessageSet msg2 = new MessageSet();
            msg2.ParsingMessage(msg.FullMessage);

            MessageSet msg3 = new MessageSet();
            msg3.MakeReqMessage("AATEST", 3000);
            msg2 = new MessageSet();
            msg2.ParsingMessage(msg3.FullMessage);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime kkk = DateTime.Now;
            TimeSpan time = DateTime.UtcNow - kkk;

            string tmp = string.Format("{0:00}", 1);

            int tmp2 = Convert.ToInt32(tmp);

        }
    }

    class MessageSet
    {
        static int messageID = 0;
        string prefix = "STX";
        string postfix = "ETX";
        string reqprefix = "STXREQ";

        public byte[] FullMessage { get; private set; }
        public TimeSpan Timeout { get; private set; } = TimeSpan.FromMilliseconds(0);
        public int MessageID { get; private set; }
        public string StringMessage { get; private set; }
        public bool IsRequest { get; private set; } = false;

        public MessageSet()
        {
            Timeout = TimeSpan.FromMilliseconds(0);
        }

        public void MakeSendMessage(string message)
        {
            StringMessage = message;
            MessageID = ++messageID;
            string strFullMessage = string.Format("{0}#{1}#{2}{3}", prefix, MessageID, message, postfix);
            FullMessage = Encoding.ASCII.GetBytes(strFullMessage);
        }

        public void MakeReqMessage(string message, int timeoutMilli)
        {
            StringMessage = message;
            IsRequest = true;
            MessageID = ++messageID;
            Timeout = TimeSpan.FromMilliseconds(timeoutMilli);
            string strFullMessage = string.Format("{0}#{1}#{2}{3}", reqprefix, MessageID, message, postfix);
            FullMessage = Encoding.ASCII.GetBytes(strFullMessage);
        }

        public void MakeRepMessage(string message, int messageID)
        {
            StringMessage = message;
            MessageID = messageID;
            string strFullMessage = string.Format("{0}#{1}#{2}{3}", prefix, MessageID, message, postfix);
            FullMessage = Encoding.ASCII.GetBytes(strFullMessage);
        }

        public void MakeAckMessage(int messageID)
        {
            StringMessage = "ACK";
            MessageID = messageID;
            string strFullMessage = string.Format("{0}#{1}#{2}{3}", prefix, MessageID, "ACK", postfix);
            FullMessage = Encoding.ASCII.GetBytes(strFullMessage);
        }

        public void ParsingMessage(byte[] data)
        {
            string strFullMessage = Encoding.ASCII.GetString(data);
            //trim prefix, postfix, reqfix
            IsRequest = strFullMessage.Substring(3, 3).Equals("REQ");
            if (IsRequest) strFullMessage = strFullMessage.Substring(6, strFullMessage.Length - 9);
            else strFullMessage = strFullMessage.Substring(3, strFullMessage.Length - 6);

            int pos = strFullMessage.IndexOf("#");
            int nextpos = strFullMessage.IndexOf("#", pos+1);
            MessageID = Convert.ToInt32(strFullMessage.Substring(pos+1, nextpos- pos-1));

            StringMessage = strFullMessage.Substring(nextpos+1);
        }
    }
}
