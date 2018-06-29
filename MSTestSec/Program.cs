using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSTest.Lib;

namespace MSTestSec
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeUtil asd = new TimeUtil();
            Console.WriteLine(asd.CurrentTime);

            Console.ReadLine();
        }
    }
}
