using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder(1024);
            Test(sb, false);
            Console.WriteLine(sb.ToString());

            Test(sb, true);
            Console.WriteLine(sb.ToString());

            Console.ReadLine();
        }

        static void Test(StringBuilder sb,  bool omitXmlDeclaration)
        {
            sb.Clear();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CheckCharacters = false;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.CloseOutput = true;
            settings.Encoding = Encoding.UTF8;

            settings.OmitXmlDeclaration = omitXmlDeclaration;

            using (XmlWriter xmlWriter = XmlWriter.Create(sb, settings))
            {
                xmlWriter.WriteStartDocument();
                // parent Hermes tag
                xmlWriter.WriteStartElement("Hermes");
                xmlWriter.WriteAttributeString("TimeStamp", DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.s"));

                xmlWriter.WriteStartElement("BoardAvailable");
                xmlWriter.WriteAttributeString("BoardId", "1");
                xmlWriter.WriteAttributeString("BoardIdCreatedBy", "2");
                xmlWriter.WriteAttributeString("FailedBoard", "3");
                xmlWriter.WriteAttributeString("ProductTypeId", "4");
                xmlWriter.WriteAttributeString("FlippedBoard", "5");
                xmlWriter.WriteEndElement();


            }
            
        }
    }
}
