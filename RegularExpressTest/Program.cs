using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RegularExpressTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "abcdefg12345hijkl12345";

            bool endsWithEx = Regex.IsMatch(test, WildCardToRegular("a?c*hijk*5"));
            bool startsWithS = Regex.IsMatch(test, WildCardToRegular("a?c*hijk*6"));
            bool containsD = Regex.IsMatch(test, WildCardToRegular("S?me*"));

            // Starts with S, ends with X, contains "me" and "a" (in that order) 
            bool complex = Regex.IsMatch(test, WildCardToRegular("S*me*a*X"));

            Console.WriteLine($"{endsWithEx}, {startsWithS}, {containsD}, {complex}");

            Console.ReadLine();
        }

        private static string WildCardToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

    }
}
