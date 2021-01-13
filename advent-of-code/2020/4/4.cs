using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using _2020;

namespace _2020_4
{
    public class PassportFieldStatus
    {
        private Func<string, bool> validationFunction;
        private bool valid;

        public PassportFieldStatus(Func<string, bool> validationFunction)
        {
            this.validationFunction = validationFunction;
        }

        public bool Present { get; private set; }
        public bool Valid { get => valid; }

        public void SetValid(string value)
        {
            Present = true;
            valid = validationFunction(value);
        }
    }

    public class Passport
    {
        private static string[] ValidColours = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        private Dictionary<string, PassportFieldStatus> fields = new Dictionary<string, PassportFieldStatus>() {
            { "byr", new PassportFieldStatus(x => {
                var number = int.Parse(x);
                return number >= 1920 && number <= 2002;
            })},
            { "iyr", new PassportFieldStatus(x => {
                var number = int.Parse(x);
                return number >= 2010 && number <= 2020;
            })},
            { "eyr", new PassportFieldStatus(x => {
                var number = int.Parse(x);
                return number >= 2020 && number <= 2030;
            })},
            { "hgt", new PassportFieldStatus(x => {
                Match match;

                if ((match = Regex.Match(x, "(\\d+)cm", RegexOptions.None)).Success)
                {
                    var number = int.Parse(match.Groups[1].ToString());

                    return number >= 150 && number <= 193;
                }
                else if ((match = Regex.Match(x, "(\\d+)in", RegexOptions.None)).Success)
                {
                    var number = int.Parse(match.Groups[1].ToString());

                    return number >= 59 && number <= 76;
                }

                return false;
            })},
            { "hcl", new PassportFieldStatus(x =>
                Regex.Match(x, "^#[0-9a-f]{6}$", RegexOptions.None).Success
            )},
            { "ecl", new PassportFieldStatus(x => ValidColours.Contains(x))},
            { "pid", new PassportFieldStatus(x =>
                Regex.Match(x, "^\\d{9}$", RegexOptions.None).Success
            )},
        };

        public Passport(string passportLine, bool validate)
        {
            var passportFields = passportLine.Trim().Split();

            foreach (var field in passportFields)
            {
                var split = field.Split(":");

                var key = split[0];
                var value = split[1];

                if (fields.TryGetValue(key, out var result))
                {
                    result.SetValid(value);
                }
            }
        }

        public bool isAllPresent()
        {
            return fields.All(kv => kv.Value.Present);
        }

        public bool isAllValid()
        {
            return fields.All(kv => kv.Value.Present && kv.Value.Valid);
        }
    }

    public static class _4
    {
        private static void Passport(bool validate)
        {
            var lines = Program.ReadAllLines(4);
            var passportLine = new StringBuilder();
            var validCount = 0;

            for (int i = 0; i <= lines.Length; i++)
            {
                if (i == lines.Length || lines[i] == "")
                {
                    var passport = new Passport(passportLine.ToString(), validate);

                    if (validate ? passport.isAllValid() : passport.isAllPresent())
                    {
                        validCount++;
                    }

                    passportLine.Clear();
                    continue;
                }
                passportLine.Append($"{lines[i]} ");
            }

            Console.WriteLine(validCount);
        }

        public static void A()
        {
            Passport(false);
        }

        public static void B()
        {
            Passport(true);
        }
    }
}