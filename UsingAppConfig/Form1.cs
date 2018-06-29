using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace UsingAppConfig
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //To use under .Net 3.5 there must have System.configuration in References
            //1. Add config file
            //2. Add using System.Configuration.
            //3. Add setting on App.config to use general config (<appSettings><add key="InitOperation" value="BOT" /></appSettings>)
            //4. Add setting on App.config to use connectionStrings config (<connectionStrings>< add name = "MG_DB" connectionString = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=tempdb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True" /></ connectionStrings >)
            //Ref. Location of config file : same with app.
            
            Console.WriteLine(Application.ProductName + ">> Append Configs[{0},{1}]", "TestName1", "TestVal1");
            AppendGeneralConfig("TestName1", "TestVal1");

            Console.WriteLine(Application.ProductName + ">> {0}:{1}", "TestName1", ConfigurationManager.AppSettings["TestName1"]);
                        
        }

        private void AppendGeneralConfig(string sKey, string sVal)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;

            if( settings.AllKeys.Contains(sKey) )
            {//modify
                settings[sKey].Value = sVal;
            }
            else
            {//append
                settings.Add(sKey, sVal);
            }            

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}
