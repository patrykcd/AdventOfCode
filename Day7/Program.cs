using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main()
        {
            var bags = File
                .ReadLines("Input.txt")
                .Select(bag => Regex.Replace(bag, "( (bags|bag)|\\.|no other)", string.Empty))
                .Select(bag => bag.Split(" contain "))
                .Select(bag => (
                    key: bag[0],
                    values: bag[1] != string.Empty ? bag[1].Split(", ") : new string[0]))
                .Select(bag => (key: bag.key, values: bag.values.Select(contents => contents.Split(' ', 2)).ToList()))
                .ToDictionary(
                    bag => bag.key,
                    contents => contents.values.Select(content => (
                        count: Convert.ToInt32(content[0]),
                        name: content[1])).ToList());

            var bagColorsCount = bags.Count(bag => Contains(bag.Value, "shiny gold"));
            var individualBagsCount = Sum(bags["shiny gold"]);

            Console.WriteLine($"Part 1: {bagColorsCount}\n" +
                              $"Part 2: {individualBagsCount}");

            bool Contains(IEnumerable<(int count, string name)> currentBag, string bagName)
            {
                var list = new List<string>();
                foreach (var (_, name) in currentBag)
                {
                    if (name == bagName) return true;
                    list.Add(name);
                }

                return list.Any(item => Contains(bags[item], bagName));
            }

            int Sum(IEnumerable<(int count, string name)> currentBag)
            {
                return currentBag.Sum(content => content.count + (content.count * Sum(bags[content.name])));
            }
        }
    }
}