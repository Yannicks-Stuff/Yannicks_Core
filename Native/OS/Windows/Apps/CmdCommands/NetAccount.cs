using System.Text.RegularExpressions;

namespace Yannick.Native.OS.Windows.Apps.CmdCommands;

public static partial class NetAccount
{
    private static readonly char[] Separator = new[] { ' ', '\t' };

    public static PasswordPoliciesInfo PasswordPolicies()
    {
        var output = CMD.CallSingleCommand("net accounts");
        var policies = new PasswordPoliciesInfo();
        var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2 && int.TryParse(parts[^1], out var value))
            {
                switch (i)
                {
                    case 1:
                        policies.MinPasswordLength = value;
                        break;
                    case 2:
                        policies.MaxPasswordAge = value;
                        break;
                    case 3:
                        policies.MinPasswordAge = value;
                        break;
                    case 4:
                        policies.PasswordHistoryLength = value;
                        break;
                    case 6:
                        policies.LockoutThreshold = value;
                        break;
                }
            }
        }

        return policies;
    }

    public partial struct PasswordPoliciesInfo
    {
        public int MinPasswordLength;
        public int MaxPasswordAge;
        public int MinPasswordAge;
        public int PasswordHistoryLength;
        public int LockoutThreshold; //TODO ADD ALL

        public bool IsPasswordCorrect(string pw)
        {
            if (pw.Length < MinPasswordLength)
                return false;

            var regex = PWMatch();

            return regex.IsMatch(pw);
        }

        [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).*$")]
        private static partial Regex PWMatch();
    }
}