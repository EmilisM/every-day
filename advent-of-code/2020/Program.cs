using System;
using System.IO;
using _2020_4;

namespace _2020
{
    class Program
    {
        static void Main(string[] args)
        {
            _4.A();
            _4.B();
        }

        public static string[] ReadAllLines(int level)
        {
            var path = Path.Join(Environment.CurrentDirectory, $"{level}\\input.txt");

            return File.ReadAllLines(path);
        }
    }
}
