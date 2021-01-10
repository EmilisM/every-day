using System;
using System.Linq;
using _2020;

namespace _2020_2
{
    public static class _2
    {
        public static void A()
        {
            var passwordsValid = Program.ReadAllLines(2).Select(line =>
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

        public static void B()
        {
            var passwordsValid = Program.ReadAllLines(2).Select(line =>
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
    }
}