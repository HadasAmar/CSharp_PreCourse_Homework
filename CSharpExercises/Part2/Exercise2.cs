using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExercises.Part2
{
    public class Exercise2
    {
        public static void Run()
        {
            Console.WriteLine($"string: abcba, {IsSortedPalindrome("abcba")}");
            Console.WriteLine($"string: abcca, {IsSortedPalindrome("abcca")}");
            Console.WriteLine($"string: abdcdba, {IsSortedPalindrome("abdcdba")}");
            Console.WriteLine($"string: abdccdba, {IsSortedPalindrome("abdccdba")}");

        }

        private static bool IsSortedPalindrome(string s)
        {
            if (s[0] != s[s.Length - 1])
                return false;
            for (int i = 1; i < s.Length / 2 + 1; i++)
            {
                if (s[i] != s[s.Length - i - 1] || s[i - 1] > s[i])
                    return false;
            }
            return true;
        }


    }
}
