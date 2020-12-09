using System;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main()
        {
            var allAnswers = File
                .ReadAllText("Input.txt")
                .Split("\n\n");

            // Part 1
            var anyoneYesAnswersSum = allAnswers
                .Select(line => line.Replace("\n", string.Empty))
                .Select(line => line.Distinct().Count())
                .Sum();

            // Part 2
            var everyoneYesAnswersSum = allAnswers
                .Select(line => line.Split('\n'))
                .Sum(group => SequenceEqual(group.ToArray()));


            Console.WriteLine($"Part 1: {anyoneYesAnswersSum}\n" +
                              $"Part 2: {everyoneYesAnswersSum}");

            int SequenceEqual(string[] arr)
            {
                var smallestStr = arr.First(str => str.Count() == arr.Min(x => x.Length));
                var sameLettersCount = 0;

                foreach (var letter in smallestStr)
                {
                    var add = arr.All(str => str.Contains(letter));
                    if (add) sameLettersCount++;
                }

                return sameLettersCount;
            }
        }
    }
}