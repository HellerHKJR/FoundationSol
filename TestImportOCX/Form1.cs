using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
//using HELLERCOMMLib;

namespace TestImportOCX
{
    public partial class Form1 : Form
    {
        AxHELLERCOMMLib.AxHellerComm hellerComm = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            const int OVEN_TYPE_HORIZONTAL = 1;
            const int OVEN_TYPE_VERTICAL = 2;

            //Type ts = Type.GetType("Form1");
            //HELLERCOMMLib.HellerComm hellerComm = (HELLERCOMMLib.HellerComm)Activator.CreateInstance(ts);

            bool ret = false;
            hellerComm = new AxHELLERCOMMLib.AxHellerComm();
            //HELLERCOMMLib.HellerComm hellerComm = new HELLERCOMMLib.HellerComm();
            hellerComm.CreateControl();


            //hellerComm.
            bool isCommunicating = false;
            isCommunicating = hellerComm.IsCommunicating();

            ret = hellerComm.StartCommunicating(1);
            isCommunicating = hellerComm.IsCommunicating();
            if (ret) ret = hellerComm.StartOven();
            isCommunicating = hellerComm.IsCommunicating();
            
            if ( hellerComm.GetOvenType() == OVEN_TYPE_HORIZONTAL)
            {
                hellerComm.NotificationEvent += HellerComm_NotificationEvent;   //11 X,
                hellerComm.Alarm += HellerComm_Alarm1;
                hellerComm.UpdateChannelParam += HellerComm_UpdateChannelParam;
                hellerComm.UpdateChannelPV += HellerComm_UpdateChannelPV;
                //hellerComm.LightTowerColor += HellerComm_LightTowerColor;   //사용.
                hellerComm.ExternalCommActive += HellerComm_ExternalCommActive;
                hellerComm.ExternalCommExited += HellerComm_ExternalCommExited;
                hellerComm.RecipeLoad += HellerComm_RecipeLoad;
                hellerComm.RunRecipe += HellerComm_RunRecipe;
                hellerComm.SaveRecipeEvent += HellerComm_SaveRecipeEvent;
                hellerComm.ConnectMode += HellerComm_ConnectMode;
                hellerComm.LargeAlarm += HellerComm_LargeAlarm;
                hellerComm.BlowerSettingChange += HellerComm_BlowerSettingChange;
                hellerComm.GenericEvent += HellerComm_GenericEvent;


                string strCurrentRecipe = string.Empty;
                hellerComm.GetRecipePath(ref strCurrentRecipe);
                
                isCommunicating = hellerComm.IsCommunicating();
                
            }
            else
            {
                Application.Exit();
            }

            //hellerComm.Dispose();


        }

        private void HellerComm_GenericEvent(object sender, AxHELLERCOMMLib._DHellerCommEvents_GenericEventEvent e)
        {
            Console.WriteLine("GenericEvent: " + e.sIdentifier);
        }

        private void HellerComm_BlowerSettingChange(object sender, AxHELLERCOMMLib._DHellerCommEvents_BlowerSettingChangeEvent e)
        {
            Console.WriteLine("BlowerSettingChange: " + e.sBlowerID + " " + e.sSpeed);
        }

        private void HellerComm_LargeAlarm(object sender, AxHELLERCOMMLib._DHellerCommEvents_LargeAlarmEvent e)
        {
            Console.WriteLine("LargeAlarm: " + e.lAlarmID);
        }

        private short HellerComm_ConnectMode(object sender, AxHELLERCOMMLib._DHellerCommEvents_ConnectModeEvent e)
        {
            Console.WriteLine("ConnectMode: " + e.enumChannel);

            return 0;
        }

        private void HellerComm_SaveRecipeEvent(object sender, AxHELLERCOMMLib._DHellerCommEvents_SaveRecipeEvent e)
        {
            Console.WriteLine("SaveRecipe: " + e.recipePath);
        }

        private void HellerComm_RunRecipe(object sender, AxHELLERCOMMLib._DHellerCommEvents_RunRecipeEvent e)
        {
            Console.WriteLine("RunRecipe: " + e.recipePath);
        }

        private void HellerComm_RecipeLoad(object sender, AxHELLERCOMMLib._DHellerCommEvents_RecipeLoadEvent e)
        {
            Console.WriteLine("RecipeLoad: " + e.recipe);
        }

        private void HellerComm_ExternalCommExited(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HellerComm_ExternalCommActive(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        //private void HellerComm_LightTowerColor(object sender, AxHELLERCOMMLib._DHellerCommEvents_LightTowerColorEvent e)
        //{
        //    Console.WriteLine(e.state);
        //}

        private void HellerComm_UpdateChannelPV(object sender, AxHELLERCOMMLib._DHellerCommEvents_UpdateChannelPVEvent e)
        {
            //throw new NotImplementedException();
        }

        private void HellerComm_Alarm1(object sender, AxHELLERCOMMLib._DHellerCommEvents_AlarmEvent e)
        {
        
        }

        private void HellerComm_UpdateChannelParam(object sender, AxHELLERCOMMLib._DHellerCommEvents_UpdateChannelParamEvent e)
        {
            
        }

        private void HellerComm_NotificationEvent(object sender, AxHELLERCOMMLib._DHellerCommEvents_NotificationEventEvent e)
        {
            switch (e.lEventID)
            {
                case 11:
                    Console.WriteLine("Noti:" + e.lEventData);
                    break;
            }
        }
        

        
        
    }
}
