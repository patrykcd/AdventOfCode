using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Passport = System.Collections.Generic.Dictionary<string, string>;

namespace Day4
{
    class Program
    {
        static void Main()
        {
            var requiredFields = new[]
            {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
                // "cid" Optional
            };

            var allPassports = File
                .ReadAllText("Input.txt")
                .Split("\n\n")
                .Select(passport => passport
                    .Replace('\n', ' ')
                    .Split(' '))
                .Select(passport => passport.Select(item => item.Split(':')))
                .Select(passport => passport.ToDictionary(
                    items => items[0],
                    items => items[1]));

            var validPassportsPart1 = allPassports.Where(IsValidPassportPart1);
            var validPassportsPart2 = validPassportsPart1.Where(IsValidPassportPart2);
            
            Console.WriteLine("Part 1: " + validPassportsPart1.Count());
            Console.WriteLine("Part 2: " + validPassportsPart2.Count());


            bool IsValidPassportPart1(Passport passport)
            {
                return passport.Select(items => items.Key)
                    .Intersect(requiredFields).Count() == requiredFields.Count();
            }
            
            bool IsValidPassportPart2(Passport passport)
            {
                return passport.Any(item =>
                           item.Key == "byr" && IsFitInRange(item.Value, 1920, 2002)) &&
                       passport.Any(item =>
                           item.Key == "iyr" && IsFitInRange(item.Value, 2010, 2020)) &&
                       passport.Any(item =>
                           item.Key == "eyr" && IsFitInRange(item.Value, 2020, 2030)) &&
                       passport.Any(item =>
                           item.Key == "hgt" && IsValidHeight(item.Value, 150, 193, 59, 76)) &&
                       passport.Any(item =>
                           item.Key == "hcl" && IsValidValue(item.Value, "^#[0-9a-f]{6}$")) &&
                       passport.Any(item =>
                           item.Key == "ecl" && IsValidValue(item.Value, "^(amb|blu|brn|gry|grn|hzl|oth){1}$")) &&
                       passport.Any(item =>
                           item.Key == "pid" && IsValidValue(item.Value, "^[0-9]{9}$"));

                bool IsFitInRange(string valueStr, int min, int max)
                {
                    var value = Convert.ToInt32(valueStr);
                    return value >= min && value <= max;
                }

                bool IsValidHeight(string heightStr, int cmMin, int cmMax, int inMin, int inMax)
                {
                    var unit = heightStr.Substring(heightStr.Length - 2, 2);
                    var isCm = unit == "cm";
                    var isIn = unit == "in";

                    if (!isCm & !isIn) return false;
                    var value = Convert.ToInt32(heightStr.Substring(0, heightStr.Length - 2));
                    if (isCm) return value >= cmMin && value <= cmMax;
                    if (isIn) return value >= inMin && value <= inMax;
                    return false;
                }

                bool IsValidValue(string value, string regex)
                {
                    return Regex.IsMatch(value, regex);
                }
            }
        }
    }
}