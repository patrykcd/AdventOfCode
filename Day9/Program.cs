using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main()
        {
            var numbers = File
                .ReadLines("Input.txt")
                .Select(number => Convert.ToUInt64(number))
                .ToArray();

            const int preamble = 25;
            for (var i = preamble; i < numbers.Length; i++)
            {
                if (!IsSumOfTwoPreviousNumbers(preamble, i))
                {
                    Console.WriteLine($"Part 1: {numbers[i]}");

                    var set = FindSetThatSumEqual(numbers[i]);
                    var min = set.Min(item => item);
                    var max = set.Max(item => item);
                    var minMaxSum = min + max;
                    Console.WriteLine($"Part 2: {minMaxSum}");

                    break;
                }
            }

            // Part 1
            bool IsSumOfTwoPreviousNumbers(int limit, int index)
            {
                limit = Math.Max(index - limit, 0);
                for (var i = index - 1; i >= limit; i--)
                {
                    for (var j = i - 1; j >= limit; j--)
                    {
                        if (numbers[i] + numbers[j] == numbers[index])
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            // Part 2
            List<ulong> FindSetThatSumEqual(ulong number)
            {
                var set = new List<ulong>();
                var sum = 0ul;

                for (var i = 0; i < numbers.Length && sum != number; i++)
                {
                    sum = 0;
                    set.Clear();
                    for (var j = i; j < numbers.Length && sum < number; j++)
                    {
                        set.Add(numbers[j]);
                        sum = set.Aggregate(0ul, (a, b) => a + b);
                    }
                }

                return set;
            }
        }
    }
}