using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexFind
{
    class Program
    {
        static void Main(string[] args)
        {

            List<RecipeTemperatureStep> tempList = new List<RecipeTemperatureStep>();
            List<RecipePressureStep> pressList = new List<RecipePressureStep>();
            List<RecipeRepeatGroup> groupList = new List<RecipeRepeatGroup>();

            for( int i = 0; i < 40; i++ )
            {
                RecipeTemperatureStep t = new RecipeTemperatureStep();

                t.Temperature = i;
                if (i < 10)
                {
                    if (i == 0 || i == 3 || i == 6)
                        t.IsRepeatStart = true;
                    if (i == 2 || i == 5 || i == 8)
                        t.IsRepeatEnd = true;
                    t.RepeatGroup = 0;

                    if (i == 9) t.RepeatGroup = -1;
                }
                else if (i < 20)
                {
                    if (i == 10 || i == 13 || i == 16)
                        t.IsRepeatStart = true;
                    if (i == 12 || i == 15 || i == 18)
                        t.IsRepeatEnd = true;
                    t.RepeatGroup = 1;

                    if (i == 19) t.RepeatGroup = -1;
                }
                else t.RepeatGroup = -1;

                tempList.Add(t);
            }

            RecipeRepeatGroup g = new RecipeRepeatGroup();
            g.IsTemperature = true;
            g.RepeatGroup = 0;
            g.RepeatCount = 3;
            groupList.Add(g);
            g = new RecipeRepeatGroup();
            g.IsTemperature = true;
            g.RepeatGroup = 1;
            g.RepeatCount = 3;
            groupList.Add(g);


            int idd = 0;
            tempList.ForEach(t =>
           {
               Console.WriteLine($"id({idd})= Value:{t.Temperature}, Start:{t.IsRepeatStart}, End:{t.IsRepeatEnd}, Group:{t.RepeatGroup}");
                   idd++;
           });
            Console.WriteLine("======================");

            groupList.ForEach(rg =>
            {
                if (rg.IsTemperature)
                {
                    //int idxStart = tempList.FindIndex(ts => ts.RepeatGroup == rg.RepeatGroup && ts.IsRepeatStart);
                    ////serch second index
                    //idxStart = tempList.FindIndex(idxStart+1, ts => ts.RepeatGroup == rg.RepeatGroup && ts.IsRepeatStart);
                    //int idxEnd = tempList.FindLastIndex(ts => ts.RepeatGroup == rg.RepeatGroup && ts.IsRepeatEnd);

                    //tempList.RemoveRange(idxStart, idxEnd- idxStart+1);

                    int idxFirstStart = tempList.FindIndex(ts => ts.RepeatGroup == rg.RepeatGroup && ts.IsRepeatStart);
                    int idxLastStart = tempList.FindLastIndex(ts => ts.RepeatGroup == rg.RepeatGroup && ts.IsRepeatStart);

                    tempList.RemoveRange(idxFirstStart, idxLastStart - idxFirstStart);

                }
                else
                {
                    //rg.RepeatGroup
                    //rg.RepeatCount
                    int idxStart = pressList.FindIndex(ts => ts.RepeatGroup == rg.RepeatGroup && ts.IsRepeatStart);
                    int idxEnd = pressList.FindIndex(ts => ts.RepeatGroup == rg.RepeatGroup && ts.IsRepeatEnd);

                }
            });

            idd = 0;
            tempList.ForEach(t =>
            {
                Console.WriteLine($"id({idd})= Value:{t.Temperature}, Start:{t.IsRepeatStart}, End:{t.IsRepeatEnd}, Group:{t.RepeatGroup}");
                idd++;
            });

            Console.ReadLine();
        }
    }

    public class RecipeTemperatureStep
    {
        public bool IsRepeatStart { get; set; } = false;
        public bool IsRepeatEnd { get; set; } = false;
        public int RepeatGroup { get; set; } = -1;
        public float Temperature { get; set; }
        public int Minute { get; set; } = 0;
        public int PID { get; set; }
    }

    public class RecipePressureStep
    {
        public bool IsRepeatStart { get; set; } = false;
        public bool IsRepeatEnd { get; set; } = false;
        public int RepeatGroup { get; set; } = -1;
        public float Pressure { get; set; }
        public int Minute { get; set; } = 0;
        public int PID { get; set; }
        public bool PressureSupply { get; set; }
        public bool VacuumSupply { get; set; }
        //public bool AirBleed { get; set; } 2019.03.18 remove
        public float VacuumSR { get; set; } = 0.1F;
    }

    public class RecipeRepeatGroup
    {
        public bool IsTemperature { get; set; } = false;
        public int RepeatGroup { get; set; } = -1;
        public int RepeatCount { get; set; } = 1;
    }
}
