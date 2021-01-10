using System;
using System.IO;

namespace _2020
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public static string[] ReadAllLines(int level)
        {
            var path = Path.Join(Environment.CurrentDirectory, $"{level}\\input.txt");

            return File.ReadAllLines(path);
        }
    }
}
