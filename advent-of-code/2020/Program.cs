using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _2020
{
    class Program
    {
        static void Main(string[] args)
        {
            _4A();
        }

        private static string[] ReadAllLines(int level)
        {
            var path = Path.Join(Environment.CurrentDirectory, $"{level}\\input.txt");

            return File.ReadAllLines(path);
        }

        public static void _1A()
        {
            var lines = ReadAllLines(1).Select(line => int.Parse(line)).ToList();


            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i; j < lines.Count; j++)
                {
                    if (lines[i] + lines[j] == 2020)
                    {
                        Console.WriteLine("{0}*{1}={2}", lines[i], lines[j], lines[i] * lines[j]);
                        return;
                    }
                }
            }
        }

        public static void _1B()
        {
            var lines = ReadAllLines(1).Select(line => int.Parse(line)).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i; j < lines.Count; j++)
                {
                    for (int k = j; k < lines.Count; k++)
                    {
                        if (lines[i] + lines[j] + lines[k] == 2020)
                        {
                            Console.WriteLine("{0}*{1}*{2}={3}", lines[i], lines[j], lines[k], lines[i] * lines[j] * lines[k]);
                            return;
                        }
                    }
                }
            }
        }

        public static void _2A()
        {
            var passwordsValid = ReadAllLines(2).Select(line =>
            {
                var lineSplit = line.Split(':');

                var policy = lineSplit[0];
                var password = lineSplit[1].Trim();

                var policySplit = policy.Split('-');
                var charSplit = policySplit[1].Split();

                var min = int.Parse(policySplit[0]);
                var max = int.Parse(charSplit[0]);
                var character = char.Parse(charSplit[1]);

                var count = password.Count(c => c == character);

                var isValid = min <= count && count <= max;

                return isValid;
            }).Count(x => x);


            Console.WriteLine(passwordsValid);
        }

        public static void _2B()
        {
            var passwordsValid = ReadAllLines(2).Select(line =>
            {
                var lineSplit = line.Split(':');

                var policy = lineSplit[0];
                var password = lineSplit[1].Trim();

                var policySplit = policy.Split('-');
                var charSplit = policySplit[1].Split();

                var firstIndex = int.Parse(policySplit[0]);
                var secondIndex = int.Parse(charSplit[0]);
                var character = char.Parse(charSplit[1]);

                return password[firstIndex - 1] == character ^ password[secondIndex - 1] == character;
            }).Count(x => x);


            Console.WriteLine(passwordsValid);
        }

        public static int _3(int right, int down)
        {
            var lines = ReadAllLines(3);

            int x, y;
            var treeCount = 0;

            for (y = 0, x = 0; y < lines.Length; y += down, x += right)
            {
                if (x >= lines[y].Length)
                {
                    x = x - lines[y].Length;
                }

                if (lines[y][x] == '#')
                {
                    treeCount++;
                }
            }

            return treeCount;
        }

        public static void _3A()
        {
            Console.WriteLine(_3(3, 1));
        }

        public static void _3B()
        {
            var result = _3(1, 1) * _3(3, 1) * _3(5, 1) * _3(7, 1) * _3(1, 2);

            Console.WriteLine(result);
        }

        public static void _4A()
        {
            var lines = ReadAllLines(4);
            var passport = new StringBuilder();
            var validCount = 0;

            for (int i = 0; i <= lines.Length; i++)
            {
                if (i == lines.Length || lines[i] == "")
                {
                    var passportString = passport.ToString();

                    if (passportString.Contains("byr") &&
                        passportString.Contains("iyr") &&
                        passportString.Contains("eyr") &&
                        passportString.Contains("hgt") &&
                        passportString.Contains("hcl") &&
                        passportString.Contains("ecl") &&
                        passportString.Contains("pid"))
                    {
                        validCount++;
                    }

                    passport.Clear();
                    continue;
                }
                passport.Append($"{lines[i]} ");
            }

            Console.WriteLine(validCount);
        }
    }
}
