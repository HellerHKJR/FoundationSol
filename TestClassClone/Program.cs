using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestClassClone
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClone t = new TestClone();
            t.a = 8;
            t.b = new List<string>();
            t.b.Add("1");
            t.b.Add("2");
            t.b.Add("3");
            t.c = new List<Dictionary<string, string>>();
            t.c.Add(new Dictionary<string, string>() { { "A", "1" } });
            
            TestClone t2 = TestClone.TestCloneClone(t);

            t.a = 10;
            t.b.Add("4");
            t.c.Add(new Dictionary<string, string>() { { "B", "2" } });
            (t.c[0])["A"] = "3";

            t.aa = 22;
            t.bb = new List<string>() { "AA", "BB" };

            TestClone t3 = TestClone.TestCloneClone2(t);

            t.a = 12;
            t.b.Add("5");

            TestClone t4 = t.Clone() as TestClone;

            t.a = 13;
            t.b.Add("6");

            Console.ReadLine();

            


        }
    }


    class TestParent
    {
        public int aa;
        public List<string> bb;
    }

    class TestClone : TestParent, ICloneable
    {
        public int a;
        public List<string> b;
        public List<Dictionary<string, string>> c;
        public TestClone()
        { }

        public static TestClone TestCloneClone(TestClone origin)
        {
            TestClone clone = new TestClone();
            clone.a = origin.a;
            clone.b = new List<string>(origin.b);
            clone.c = new List<Dictionary<string, string>>(origin.c);

            return clone;
        }

        public static TestClone TestCloneClone2(TestClone origin)
        {
            
            return (TestClone)origin.MemberwiseClone();
        }

        public object Clone()
        {
            TestClone clone = new TestClone();
            clone.a = this.a;
            clone.b = this.b;

            return clone;
        }
    }
}
