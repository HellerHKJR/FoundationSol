using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateRealStepNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Step> PressureStepList = new List<Step>();
            PressureStepList.Add(new Step(-1));
            PressureStepList.Add(new Step(-1));
            PressureStepList.Add(new Step(0));
            PressureStepList.Add(new Step(0));
            PressureStepList.Add(new Step(0));
            PressureStepList.Add(new Step(1));
            PressureStepList.Add(new Step(1));
            PressureStepList.Add(new Step(2));
            PressureStepList.Add(new Step(2));
            PressureStepList.Add(new Step(2));
            //PressureStepList.Add(new Step(-1));

            List<RepeatGroupList> RepeatGroups = new List<RepeatGroupList>();
            RepeatGroups.Add(new RepeatGroupList(0, 2));
            RepeatGroups.Add(new RepeatGroupList(1, 3));
            //RepeatGroups.Add(new RepeatGroupList(1, 3));

            int lastRepeattimesP = 0;
            int totalStep = 0;
            int lastRepeatGroup = -1;
            List<List<int>> stepNoSet = new List<List<int>>();
            List<int> innerStepNoSet = new List<int>();
            PressureStepList.ForEach(ps =>
            {
                if (ps.RepeatGroup < 0)
                {

                    if (innerStepNoSet.Count > 0)
                    {
                        innerStepNoSet.ForEach(i => 
                        {
                            List<int> madeStep = new List<int>();

                            for (int j = 0; j < lastRepeattimesP; j++)
                            {
                                totalStep = i + (j * innerStepNoSet.Count);
                                madeStep.Add(totalStep);
                            }

                            stepNoSet.Add(madeStep);

                        });

                    }

                    innerStepNoSet.Clear();
                    lastRepeattimesP = 0;

                    stepNoSet.Add(new List<int>() { ++totalStep });

                    
                }
                else
                {
                    if (lastRepeatGroup != ps.RepeatGroup && lastRepeatGroup > -1)
                    {
                        innerStepNoSet.ForEach(i =>
                        {
                            List<int> madeStep = new List<int>();

                            for (int j = 0; j < lastRepeattimesP; j++)
                            {
                                totalStep = i + (j * innerStepNoSet.Count);
                                madeStep.Add(totalStep);
                            }

                            stepNoSet.Add(madeStep);

                        });

                        innerStepNoSet.Clear();
                        lastRepeattimesP = 0;
                    }

                    lastRepeatGroup = ps.RepeatGroup;
                    innerStepNoSet.Add(++totalStep);
                   
                    try
                    {
                        if (lastRepeattimesP < 1)
                            lastRepeattimesP = RepeatGroups.Where(rg => rg.RepeatGroup == ps.RepeatGroup).First().RepeatTimes;
                    }
                    catch
                    {
                        lastRepeattimesP = 1;
                    }
                }
            });

            if (innerStepNoSet.Count > 0)
            {
                innerStepNoSet.ForEach(i =>
                {
                    List<int> madeStep = new List<int>();

                    for (int j = 0; j < lastRepeattimesP; j++)
                    {
                        totalStep = i + (j * innerStepNoSet.Count);
                        madeStep.Add(totalStep);
                    }

                    stepNoSet.Add(madeStep);

                });

                innerStepNoSet.Clear();
                lastRepeattimesP = 0;
            }


            for ( int i = 0; i < PressureStepList.Count; i++ )
            {
                PressureStepList[i].StepNoList = stepNoSet[i];

            }

            Console.WriteLine();
            
        }
    }



    public class Step
    {
        public Step(int rg)
        {
            RepeatGroup = rg;
        }
        public List<int> StepNoList { get; set; } = new List<int>();
        public int RepeatGroup { get; set; }
    }


    public class RepeatGroupList
    {
        public RepeatGroupList(int groupNo, int rptCount)
        {
            RepeatGroup = groupNo;
            RepeatTimes = rptCount;
        }
        public int RepeatGroup { get; set; }
        public int RepeatTimes { get; set; }
    }
}
