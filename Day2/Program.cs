using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main()
        {
            string line;
            using var stream = File.OpenText("Input.txt");
            var validPasswords1 = 0;
            var validPasswords2 = 0;
            while ((line = stream.ReadLine()) != null)
            {
                var data = line
                    .Replace('-', ' ')
                    .Replace(":", String.Empty)
                    .Split(' ');

                var min = Convert.ToInt32(data[0]);
                var max = Convert.ToInt32(data[1]);
                var letter = Convert.ToChar(data[2]);
                var str = data[3];

                // Part 1
                var count = str.Count(item => item == letter);
                if (count >= min && count <= max)
                {
                    validPasswords1++;
                }
                
                // Part 2
                var firstPosition = min;
                var secondPosition = max;
                var firstLetter = str[firstPosition - 1];
                var secondLetter = str[secondPosition - 1];
                if (firstLetter == letter && secondLetter == letter) continue;
                if (firstLetter == letter || secondLetter == letter) validPasswords2++;
            }
            
            Console.WriteLine($"Part 1: {validPasswords1}\n" + 
                              $"Part 2: {validPasswords2}");
        }
    }
}