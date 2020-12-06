using System;
using System.Collections.Generic;
using System.IO;

namespace Day5
{
    class Program
    {
        private delegate void HalfOf(ref (int first, int last) range);

        static void Main()
        {
            var taskDictionary = new Dictionary<char, HalfOf>()
            {
                {'B', UpperHalfOf},
                {'F', LowerHalfOf},
                {'R', UpperHalfOf},
                {'L', LowerHalfOf}
            };

            var stream = File.OpenText("Input.txt");
            var max = int.MinValue;
            var min = int.MaxValue;
            var sum = 0;
            var count = 1;
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                (int first, int last) rowRange = (0, 127);
                foreach (var letter in line.Substring(0, 7))
                {
                    taskDictionary[letter](ref rowRange);
                }

                (int first, int last) columnRange = (0, 7);
                foreach (var letter in line.Substring(7, 3))
                {
                    taskDictionary[letter](ref columnRange);
                }

                var result = rowRange.first * 8 + columnRange.first;
                max = Math.Max(result, max);
                min = Math.Min(result, min);
                sum += result;
                count++;
            }

            var sumOfAnArithmeticSequence = (min + max) / 2 * count;
            var missingValue = sumOfAnArithmeticSequence - sum;

            Console.WriteLine($"Part 1: {max}");
            Console.WriteLine($"Part 2: {missingValue}");

            void UpperHalfOf(ref (int first, int last) range)
            {
                range.first = (int) Math.Ceiling(range.first + (range.last - range.first) / 2f);
            }

            void LowerHalfOf(ref (int first, int last) range)
            {
                range.last = (int) Math.Floor((range.first + range.last) / 2f);
            }
        }
    }
}