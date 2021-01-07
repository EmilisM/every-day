using System;
using System.IO;
using System.Linq;

namespace _2020
{
    class Program
    {
        static void Main(string[] args)
        {
            _3A();
            _3B();
        }

        private static string[] Read(int level)
        {
            var path = Path.Join(Environment.CurrentDirectory, $"{level}\\input.txt");

            return File.ReadAllLines(path);
        }

        public static void _1A()
        {
            var lines = Read(1).Select(line => int.Parse(line)).ToList();


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
            var lines = Read(1).Select(line => int.Parse(line)).ToList();

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
            var passwordsValid = Read(2).Select(line =>
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
            var passwordsValid = Read(2).Select(line =>
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
            var lines = Read(3);

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
            System.Console.WriteLine(_3(3, 1));
        }

        public static void _3B()
        {
            var result = _3(1, 1) * _3(3, 1) * _3(5, 1) * _3(7, 1) * _3(1, 2);

            System.Console.WriteLine(result);
        }
    }
}
