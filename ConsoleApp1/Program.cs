using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class SomeThing
    {
        public string someVal = "SSS";
    }

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("AAAAA {0}", i);
                ProcessSSS();
            }

            SomeThing s = null;
            Console.WriteLine("SSSSSSSSSSS");
            Console.WriteLine("SSS".Equals(s?.someVal) ? "!!!!!" : "?????" );
            Console.WriteLine("SSSSSSSSSSS");

            int kk = Convert.ToInt32("");


            Console.ReadLine();

        }


        static void ProcessSSS()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");
            Console.WriteLine("SSS2");
            Console.WriteLine("SSS3");
            Console.WriteLine("SSS4");
            Console.WriteLine("SSS5");

            ProcessSSS1();
        }

        static void ProcessSSS1()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");

            ProcessSSS2();
        }

        static void ProcessSSS2()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");

            ProcessSSS3();
        }

        static void ProcessSSS3()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");

            ProcessSSS4();
        }
        static void ProcessSSS4()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");

            

            ProcessSSS5();
        }
        static void ProcessSSS5()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");
            
        }
        static void ProcessSSS6()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");
        }
        static void ProcessSSS7()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");
        }
        static void ProcessSSS8()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");
        }
        static void ProcessSSS9()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");
        }
        static void ProcessSSS10()
        {
            Console.WriteLine("SSS0");
            Console.WriteLine("SSS1");
        }
        
    }
    
}
