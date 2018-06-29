using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DateTimeToString
{
    class Program
    {
        static void Main(string[] args)
        {
            

            for (int i = 0; i < 100; i++ )
            {
                DateTime dt = DateTime.Now;
                string rtn = string.Format("{0:HH:mm:ss.fff}", dt);

                Console.WriteLine(rtn);
                Thread.Sleep(500);
            }
            Console.ReadLine();


        }
    }
}
