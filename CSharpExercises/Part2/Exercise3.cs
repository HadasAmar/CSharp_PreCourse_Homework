using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExercises.Part2
{
    public class Exercise3
    {
        public static void Run()
        {
            InputNumbers();
        }

        private static void InputNumbers()
        {
            double num;
            string input;
            List<double> numbers = new();
            Console.WriteLine("Enter numbers (to end enter -1):");

            while (true)
            {
                input = Console.ReadLine();

                if (double.TryParse(input, out num))
                {
                    if (num == -1) // תנאי עצירה
                        break;

                    numbers.Add(num);
                }
                else
                {
                    Console.WriteLine("Input not correct, try again");
                }
            }

            if (numbers.Count == 0)
            {
                Console.WriteLine("No numbers entered");
                return;
            }

            double avg = numbers.Average();
            int positiveCount = numbers.Count(x => x > 0);
            numbers.Sort();

            Console.WriteLine($"Average: {avg}");
            Console.WriteLine($"Positive count: {positiveCount}");
            Console.WriteLine($"Sorted list: {string.Join(" ", numbers)}");
        }
    }
}
