using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UsingProperties
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Use User Properties
            //1. Add "Settings File" and move to Properties Directory on Solution Explorer.
            //2. Open SettingsFile and edit some properties.
            //3. Ref) Type is nomal Type used in c#, Scope is one of Application and User, Value is default value for use.
            //4. Ref) This make app.config for link property and application as same things.
            //4. Ref) Location of Setting : C:\Users\%username%\AppData\Local\%ProjectName%\%ProjectUUID%\%version%

            ////////////////////////////
            //Use User Scope
            //Use Properties
            Console.WriteLine( Properties.MySetting.Default["TestProp"] );

            //Change and Save Properties
            Properties.MySetting.Default["TestProp"] = "KKK";
            Properties.MySetting.Default.Save();
            
            //Use Properties
            Console.WriteLine(Properties.MySetting.Default["TestProp"]);

            ////////////////////////////
            //Use Applicaton Scope
            Console.WriteLine(Properties.MySetting.Default["TestPropApp"]);
                        
            Properties.MySetting.Default["TestPropApp"] = "AAAA";   //It is only valid when app is running.
            //Properties.MySetting.Default.Save();  //can not save. 
            
            Console.WriteLine(Properties.MySetting.Default["TestPropApp"]);

        }
    }
}
