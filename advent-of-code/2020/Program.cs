using System;
using System.IO;
using System.Linq;

namespace _2020
{
    class Program
    {
        static void Main(string[] args)
        {
            _1A();
            _1B();
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

        public static string[] Read(int level)
        {
            var path = Path.Join(Environment.CurrentDirectory, $"{level}\\input.txt");

            return File.ReadAllLines(path);
        }
    }
}
