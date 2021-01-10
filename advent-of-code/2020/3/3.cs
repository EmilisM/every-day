using System;
using _2020;

namespace _2020_3
{
    public static class _3
    {
        public static int Slope(int right, int down)
        {
            var lines = Program.ReadAllLines(3);

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
            Console.WriteLine(Slope(3, 1));
        }

        public static void _3B()
        {
            var result = Slope(1, 1) * Slope(3, 1) * Slope(5, 1) * Slope(7, 1) * Slope(1, 2);

            Console.WriteLine(result);
        }
    }
}