using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisposeTest
{
    class Class1 : IDisposable
    {
        public int k = 0;

        public void Dispose()
        {
            Console.WriteLine("Now Disposing!!!!!!!");
        }
    }
}
