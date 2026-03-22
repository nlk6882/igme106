using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PE3_Recursion_NolanK
{
    internal class Program
    {
        
        public static void Main()
        {
            // local variables for testing our recursive methods
            const int NumElements = 5;
            int[] nums = new int[NumElements];
            int[] numsReverse = new int[NumElements];
            int[] numsRandom = new int[NumElements];
            Random rng = new Random();
            string word;

            // Setup 3 arrays - nums in order, nums in reverse order, nums with random values
            for (int i = 0; i < NumElements; i++)
            {
                nums[i] = i;
                numsReverse[i] = NumElements - i - 1;
                numsRandom[i] = rng.Next(0, NumElements * 3);
            }

            // Put the number 42 at a random location in the non-random arrays
            nums[rng.Next(NumElements)] = 42;
            numsReverse[rng.Next(NumElements)] = 42;

            // Print each array
            PrintArray("In order", nums);
            PrintArray("In reverse", numsReverse);
            PrintArray("Random", numsRandom);
            Console.WriteLine();

            // Calc the factorial of each random number
            for (int i = 0; i < NumElements; i++)
            {
                Console.WriteLine($"{numsRandom[i]}! = {Factorial(numsRandom[i])}");
            }
            Console.WriteLine();

            // Sum the elements of each array
            Console.WriteLine($"Sum of nums is {Sum(nums)}");
            Console.WriteLine($"Sum of numsReverse is {Sum(numsReverse)}");
            Console.WriteLine($"Sum of numsRandom is {Sum(numsRandom)}");
            Console.WriteLine();

            // Find if the number 3 is in each array
            Console.WriteLine($"Contains 3 in nums: {Contains(nums, 3)}");
            Console.WriteLine($"Contains 3 in numsReverse: {Contains(numsReverse, 3)}");
            Console.WriteLine($"Contains 3 in numsRandom: {Contains(numsRandom, 3)}");
            Console.WriteLine();

            // Find if the number 42 is in each array
            Console.WriteLine($"Contains 42 in nums: {Contains(nums, 42)}");
            Console.WriteLine($"Contains 42 in numsReverse: {Contains(numsReverse, 42)}");
            Console.WriteLine($"Contains 42 in numsRandom: {Contains(numsRandom, 42)}");
            Console.WriteLine();

            // Prompt the user for a word to test string methods
            Console.WriteLine("Enter a word:");
            word = Console.ReadLine();
            Console.WriteLine($"Is {word} a palindrome? {IsPalindrome(word)}");
            Console.WriteLine($"Reverse of {word} is {Reverse(word)}");
        }

        private static void PrintArray(string label, int[] arrayData)
        {
            Console.WriteLine(label + " ");
            foreach(int i in arrayData) Console.Write(i + " ");
            Console.WriteLine();

        }

        private static int Factorial(int num)
        {
            if (num <= 1)
            {
                return 1;
            }
            else
            {
                return num * Factorial(num - 1);
            }
            
        }

        //todo
        private static int Sum(int[] arrayData)
        {
            if (arrayData.Length <= 1)
            {
                return arrayData[0];
            }
            else 
            {
                return arrayData[0] + Sum(arrayData[1..]);
            }

        }

        private static bool Contains(int[] numList, int numToFind)
        {
            if (numList == null || numList.Length == 0)
            {
                return false;
            }
            else if (numList[0] == numToFind)
            {
                return true;
            }
            else
            {
                //numlist [1..] gets subarray from index 1 to end of the array
                return Contains(numList[1..], numToFind);
            }


        }

        //todo
        private static bool IsPalindrome(string word)
        {
            if (Reverse(word) == word)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //todo
        private static string Reverse(string word)
        {
            return word;
        }




    }
}
