using System;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main()
        {
            using var stream = File.OpenText("Input.txt");
            string line;

            var steps = new (int x, int y)[]
            {
                (1, 1), // Part 2
                (3, 1), // Part 1
                (5, 1), // Part 2
                (7, 1), // Part 2
                (1, 2)  // Part 2
            };

            var treesCounts = new int[steps.Length];
            var xPerStep = new int[steps.Length];
            var y = 1;
            stream.ReadLine();
            while ((line = stream.ReadLine()) != null)
            {
                for (var i = 0; i < xPerStep.Length; i++)
                {
                    if (y % steps[i].y == 0)
                    {
                        xPerStep[i] = (xPerStep[i] + steps[i].x) % line.Length;
                        if (line[xPerStep[i]] == '#') treesCounts[i]++;
                    }

                    y++;
                }
            }

            var product = treesCounts.Aggregate(1L, (a, b) => a * b);
            Console.WriteLine(product);
        }
    }
}