using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Yannick.Secure
{
    public class Password
    {
        private static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

        public static string Generate(int length, int numberOfNonAlphanumericCharacters)
        {
            if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
            {
                throw new ArgumentException(nameof(numberOfNonAlphanumericCharacters));
            }

            using (var rng = RandomNumberGenerator.Create())
            {
                var byteBuffer = new byte[length];

                rng.GetBytes(byteBuffer);

                var count = 0;
                var characterBuffer = new char[length];

                for (var iter = 0; iter < length; iter++)
                {
                    var i = byteBuffer[iter] % 87;

                    if (i < 10)
                    {
                        characterBuffer[iter] = (char)('0' + i);
                    }
                    else if (i < 36)
                    {
                        characterBuffer[iter] = (char)('A' + i - 10);
                    }
                    else if (i < 62)
                    {
                        characterBuffer[iter] = (char)('a' + i - 36);
                    }
                    else
                    {
                        characterBuffer[iter] = Punctuations[i - 62];
                        count++;
                    }
                }

                if (count >= numberOfNonAlphanumericCharacters)
                {
                    return new string(characterBuffer);
                }

                int j;
                var rand = new Random();

                for (j = 0; j < numberOfNonAlphanumericCharacters - count; j++)
                {
                    int k;
                    do
                    {
                        k = rand.Next(0, length);
                    } while (!char.IsLetterOrDigit(characterBuffer[k]));

                    characterBuffer[k] = Punctuations[rand.Next(0, Punctuations.Length)];
                }

                return new string(characterBuffer);
            }
        }

        public class RegexRule
        {
            public RegexRule(string name, string message, string pattern)
            {
                Name = name;
                Message = message;
                // Compile the regex for performance
                CompiledRegex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
            }

            public string Name { get; }
            public string Message { get; }
            public Regex CompiledRegex { get; }
        }

        public class Rules
        {
            public static readonly RegexRule NumbersRule =
                new RegexRule("Numbers", "Enter a number between 0 and 9", "^[0-9]+$");

            // Stores tuples: (rule, min, max, optional)
            private readonly List<(RegexRule Rule, uint Min, uint Max, bool Optional)> _rules
                = new List<(RegexRule, uint, uint, bool)>();

            /// <summary>
            /// Add a new rule with optional constraints for minimum/maximum length,
            /// and an optional flag indicating whether the input can be empty or missing.
            /// </summary>
            public void Add(RegexRule rule, uint min = 0, uint max = 0, bool optional = false)
            {
                if (rule == null)
                    throw new ArgumentNullException(nameof(rule));

                _rules.Add((rule, min, max, optional));
            }

            /// <summary>
            /// Validates the input string against all added rules.
            /// Returns true if all rules pass, otherwise false.
            /// </summary>
            public bool IsStringValid(string input)
            {
                foreach (var (rule, min, max, optional) in _rules)
                {
                    // 1) If this rule is optional and the input is empty/null, skip it
                    if (optional && string.IsNullOrEmpty(input))
                        continue;

                    // 2) If it's not optional, the input must not be empty
                    if (!optional && string.IsNullOrEmpty(input))
                        return false;

                    // 3) Check length constraints first
                    var length = input?.Length ?? 0;
                    if (min != 0 && length < min)
                        return false;
                    if (max != 0 && length > max)
                        return false;

                    // 4) Check regex pattern
                    if (!rule.CompiledRegex.IsMatch(input))
                        return false;
                }

                return true;
            }
        }
    }
}