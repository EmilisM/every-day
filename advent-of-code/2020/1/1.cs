using System;
using System.Linq;
using _2020;

namespace _2020_1
{
    public static class _1
    {
        public static void A()
        {
            var lines = Program.ReadAllLines(1).Select(line => int.Parse(line)).ToList();

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

        public static void B()
        {
            var lines = Program.ReadAllLines(1).Select(line => int.Parse(line)).ToList();

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
    }
}