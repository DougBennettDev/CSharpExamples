using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberReversal
{
    public static class SequenceOfNumbers
    {
        private static Stack<int> currentNumbers = new Stack<int>();
        private static List<int> numbers = new List<int>();

        public static void AddNumbers()
        {
            Console.Write("Enter amount of numbers to reverse: ");
            int numbersLength = int.Parse(Console.ReadLine());

            if (numbersLength <= 0)
            {
                throw new ArgumentOutOfRangeException("Numbers length cannot negative or zero. " +
                    "You have to enter at least 1 number and up to 15 numbers");
            }

            for (int i = 0; i < numbersLength; i++)
            {
                Console.Write("Enter number: ");
                numbers.Add(int.Parse(Console.ReadLine()));
            }
        }

        public static void ReverseNumbers()
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                currentNumbers.Push(numbers[i]);
            }

            numbers.Clear();

            while (currentNumbers.Count > 0)
            {
                numbers.Add(currentNumbers.Pop());
            }
        }

        public static void PrintNumbers()
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                Console.Write(numbers[i] + " ");
            }

            Console.WriteLine();
        }
    }
}