using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day1
{
    class Program
    {
        static void Main()
        {
            var input = File
                .ReadAllLines("Input.txt", Encoding.UTF8)
                .Select(x => Convert.ToInt32(x))
                .ToArray();

            var completedTasks = 0;
            for (var x = 0; x < input.Length; x++)
            {
                for (var y = x + 1; y < input.Length; y++)
                {
                    // Part 1
                    if (input[x] + input[y] == 2020)
                    {
                        var product = input[x] * input[y];
                        Console.WriteLine($"{input[x]} * {input[y]} = {product}");
                        completedTasks++;
                        if (completedTasks == 2)
                        {
                            return;
                        }
                    }

                    for (var z = y + 1; z < input.Length; z++)
                    {
                        // Part 2
                        if (input[x] + input[y] + input[z] == 2020)
                        {
                            var product = input[x] * input[y] * input[z];
                            Console.WriteLine($"{input[x]} * {input[y]} * {input[z]} = {product}");
                            completedTasks++;
                            if (completedTasks == 2)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
