using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LazyLoadProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            Lazy<MyClass> MyLazyClass = new Lazy<MyClass>( () => new MyClass() ); // create lazy class
            Console.WriteLine("IsValueCreated = {0}", MyLazyClass.IsValueCreated); // print value to check if initialization is over

            MyClass sample = MyLazyClass.Value; // real value Creation Time
            Console.WriteLine("Length = {0}", sample.Length); // print array length

            Console.WriteLine("IsValueCreated = {0}", MyLazyClass.IsValueCreated); // print value to check if initialization is over


            MyClass sample2 = MyLazyClass.Value; // real value Creation Time
            Console.WriteLine("Length = {0}", sample2.Length); // print array length


            Console.ReadLine();
        }
    }

    class MyClass
    {
        int[] array;
        public MyClass()
        {
            array = new int[10];

            
            Console.WriteLine("MyClass initiated");

        }

        public int Length
        {
            get
            {
                return this.array.Length;
            }
        }
    }
}




