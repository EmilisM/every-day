using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using _2020;

namespace _2020_4
{
    public static class _4
    {
        public static void A()
        {
            var lines = Program.ReadAllLines(4);
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

        public static string[] ValidColours = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        public static void B()
        {
            var lines = Program.ReadAllLines(4);
            var passport = new StringBuilder();
            var validCount = 0;

            for (int i = 0; i <= lines.Length; i++)
            {
                if (i == lines.Length || lines[i] == "")
                {
                    var passportSplit = passport.ToString().Trim().Split();

                    if (passportSplit.Select(ValidatePassportField).All(x => x))
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

        public static bool ValidatePassportField(string field)
        {
            var split = field.Split(":");

            var key = split[0];
            var value = split[1];

            switch (key)
            {
                case "byr":
                    {
                        var number = int.Parse(value);
                        return number >= 1920 && number <= 2002;
                    }
                case "iyr":
                    {
                        var number = int.Parse(value);
                        return number >= 2010 && number <= 2020;
                    }
                case "eyr":
                    {
                        var number = int.Parse(value);
                        return number >= 2020 && number <= 2030;
                    }
                case "hgt":
                    {
                        Match match;

                        if ((match = Regex.Match(value, "(\\d{3})cm", RegexOptions.None)).Success)
                        {
                            var number = int.Parse(match.Groups[1].ToString());

                            return number >= 150 && number <= 193;
                        }
                        else if ((match = Regex.Match(value, "(\\d{2})in", RegexOptions.None)).Success)
                        {
                            var number = int.Parse(match.Groups[1].ToString());

                            return number >= 59 && number <= 76;
                        }

                        return false;
                    }
                case "hcl":
                    {
                        return Regex.Match(value, "^#[0-9a-f]{6}$", RegexOptions.None).Success;
                    }
                case "ecl":
                    {
                        return ValidColours.Contains(value);
                    }
                case "pid":
                    {
                        return Regex.Match(value, "^\\d{9}$", RegexOptions.None).Success;
                    }
                case "cid":
                    {
                        return true;
                    }
            }

            return false;
        }
    }
}