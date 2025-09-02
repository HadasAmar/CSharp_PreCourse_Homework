using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExercises.Part2
{
    public class Exercise1
    {
        public static void Run()
        {
          
            Console.WriteLine($"number: 1, number of digits: {GetNumLength(1)}");
            Console.WriteLine($"number: 123, number of digits: {GetNumLength(123)}");
            Console.WriteLine($"number: 1000, number of digits: {GetNumLength(1000)}");
        }

        private static int GetNumLength(int number)
        {
            return (int) Math.Log10(number) + 1;
        }


    }
}
