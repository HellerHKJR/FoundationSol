using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebClientSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.1.201";

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString(url);

            Console.WriteLine(html);

            ////Html Agility Pack
            //HtmlWeb client = new HtmlWeb();
            //HtmlDocument doc = client.Load("http://www.google.com");
            //HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//a[@href]");

            //foreach (var link in Nodes)
            //{
            //    Console.WriteLine(link.Attributes["href"].Value);
            //}
        }
    }
}
