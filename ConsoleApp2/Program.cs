using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] a = new string[] { "1", "2" };

            Console.WriteLine(Convert.ToDouble(a[0]));
            Console.WriteLine(Convert.ToDouble(a[1]));

            try
            {
                Console.WriteLine(Convert.ToDouble(a[2]));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("End");

            Console.ReadLine();
        }
    }
}
