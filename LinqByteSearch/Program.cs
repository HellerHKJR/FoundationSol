using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqByteSearch
{
    class Program
    {

        public static void printSome(List<byte> some)
        {

            Console.WriteLine(Encoding.Unicode.GetString(some.ToArray()));

        }

        static List<byte> keep = null;
        public static void parseSome(byte[] eles)
        {
            List<byte> tmp = null;
            if (keep != null && keep.Count != 0) tmp = keep;
            foreach (byte e in eles)
            {
                if (tmp == null) tmp = new List<byte>();

                if (tmp != null && e != 0x02 && e != 0x03) tmp.Add(e);
                if (e == 0x02) tmp.Clear();
                if (e == 0x03)
                {
                    printSome(tmp);

                    tmp.Clear();
                }
            }

            if (tmp != null && tmp.Count != 0)
            {
                keep = tmp;
            }
        }

        static void Main(string[] args)
        {

            byte[] buffer = new byte[] { 0x43, 0x03, 0x02, 0x41, 0x42, 0x03, 0x02, 0x41, 0x42, 0x43, 0x44 , 0x45, 0x03, 0x02, 0x55};

            parseSome(buffer);
            parseSome(buffer);

            Console.WriteLine("=======================================");




            IEnumerable<byte> lastFragged = from ele in buffer where ele == 0x03 select ele;


            var windows = buffer.Select((ele, i) => buffer.Skip(i).Take(buffer.Length));
            
            var endmatched = windows.Where(w => w.Last() == 0x03);
            var fisrtmatched = windows.Where(w => w.First() == 0x02);

            var allcombinations = fisrtmatched.SelectMany(
                m => Enumerable.Range(1, m.Count()).Select(
                    i => m.Take(i)).Where(x => x.Count() > 2 && x.Last() == 0x03) );


            foreach( var some in allcombinations)
            {
                byte[] ss = some.ToArray();

                Console.WriteLine(ss);
            }

            //byte[][] rrr = allcombinations.ToList();
            Console.WriteLine(allcombinations.ToArray());
        }
    }
}
