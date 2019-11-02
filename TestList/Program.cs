using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestList
{
    public class TempObjectView
    {
        public object ObjectValue { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TempObjectView tt1 = new TempObjectView();
            TempObjectView tt2 = new TempObjectView();
            TempObjectView tt3 = new TempObjectView();

            List<string> dinosaurs = new List<string>();
            
            dinosaurs.Add("Compsognathus");
            dinosaurs.Add("Amargasaurus");
            dinosaurs.Add("Oviraptor");
            dinosaurs.Add("Velociraptor");
            dinosaurs.Add("Deinonychus");
            dinosaurs.Add("Dilophosaurus");
            dinosaurs.Add("Gallimimus");
            dinosaurs.Add("Triceratops");

            tt1.ObjectValue = dinosaurs;
            dinosaurs = new List<string>();

            dinosaurs.Add("Compsognathus");
            dinosaurs.Add("Amargasaurus");
            tt2.ObjectValue = dinosaurs;
            dinosaurs = new List<string>();

            dinosaurs.Add("Dilophosaurus");
            dinosaurs.Add("Gallimimus");
            dinosaurs.Add("Triceratops");
            tt3.ObjectValue = dinosaurs;



            foreach ( string d in dinosaurs)
            {
                if (d.Equals("Velociraptor"))
                    break;

                Console.WriteLine(d);
            }
            Console.WriteLine("===========================");
            Console.WriteLine();

            dinosaurs.ForEach(d =>
            {
                if (d.Equals("Velociraptor"))
                    return;

                Console.WriteLine(d);
            });

            Console.WriteLine("===========================");
            Console.WriteLine();
            foreach (string dinosaur in dinosaurs)
            {
                Console.WriteLine(dinosaur);
            }

            try
            {
                var gongyong = dinosaurs.Count(d => d == "ABCD");

                
                Console.WriteLine(gongyong);
            }
            catch
            {
                Console.WriteLine("no gongyong");
            }



            int idxFind = dinosaurs.FindIndex(l => l.Equals("Oviraptor"));
            dinosaurs.RemoveRange(0, idxFind + 1);

            Console.WriteLine();
            foreach (string dinosaur in dinosaurs)
            {
                Console.WriteLine(dinosaur);
            }



            Console.WriteLine("\nTrueForAll(EndsWithSaurus): {0}",
                dinosaurs.TrueForAll(EndsWithSaurus));

            Console.WriteLine("\nFind(EndsWithSaurus): {0}",
                dinosaurs.Find(EndsWithSaurus));

            Console.WriteLine("\nFindLast(EndsWithSaurus): {0}",
                dinosaurs.FindLast(EndsWithSaurus));

            Console.WriteLine("\nFindAll(EndsWithSaurus):");
            List<string> sublist = dinosaurs.FindAll(EndsWithSaurus);

            foreach (string dinosaur in sublist)
            {
                Console.WriteLine(dinosaur);
            }

            Console.WriteLine(
                "\n{0} elements removed by RemoveAll(EndsWithSaurus).",
                dinosaurs.RemoveAll(EndsWithSaurus));

            Console.WriteLine("\nList now contains:");
            foreach (string dinosaur in dinosaurs)
            {
                Console.WriteLine(dinosaur);
            }

            Console.WriteLine("\nExists(EndsWithSaurus): {0}",
                dinosaurs.Exists(EndsWithSaurus));


            List<string> testStringWithLF = new List<string>();
            testStringWithLF.Add("SS");
            testStringWithLF.Add("YY");
            testStringWithLF.Add("XX");
            bool isFirstAddr = true;
            string retBuffer = "";
            testStringWithLF.ForEach(addr =>
            {
                if (isFirstAddr) retBuffer += addr;
                else retBuffer += "\n" + addr;
                isFirstAddr = false;
            });

            Console.WriteLine(retBuffer);

            Dictionary<string, int> testDic = new Dictionary<string, int>();
            testDic.Add("SS", 1);
            testDic.Add("YY", 2);
            testDic.Add("XX", 3);
            List<int> lstData = new List<int>();
            isFirstAddr = true;
            retBuffer = "";
            testDic.All(kv =>
            {
                if (isFirstAddr) retBuffer += kv.Key;
                else retBuffer += "\n" + kv.Key;
                isFirstAddr = false;

                lstData.Add(kv.Value);

                return true;
            });

            //if (testDic.Remove("SS")) Console.WriteLine("Remove Success");
            //else Console.WriteLine("Remove Fail");
            //if (testDic.Remove("QWER")) Console.WriteLine("Remove Success");
            //else Console.WriteLine("Remove Fail");

            Console.WriteLine($"{testDic.FirstOrDefault(p => p.Value == 3).Key}");
            Console.WriteLine($"{testDic.FirstOrDefault(p => p.Value == 4).Key}");

            Console.ReadLine();
        }

        // Search predicate returns true if a string ends in "saurus".
        private static bool EndsWithSaurus(String s)
        {
            return s.ToLower().EndsWith("saurus");
        }
    }
}
