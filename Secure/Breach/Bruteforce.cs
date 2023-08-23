using System.Text;
using Yannick.Lang;

namespace Yannick.Secure.Breach;

/// <summary>
/// Represents a class for brute-forcing passwords.
/// </summary>
public sealed class Bruteforce
{
    /// <summary>
    /// Starts the brute-force process asynchronously.
    /// </summary>
    /// <param name="convert">A function to convert the password attempt to the desired format.</param>
    /// <param name="password">The target password to find.</param>
    /// <param name="startLength">The starting length of the password attempt.</param>
    /// <param name="length">The maximum length of the password attempt.</param>
    /// <param name="alphabet">The character set used for password attempts. If null, the default alphabet is used.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter contains the found password or null if not found.</returns>
    public static async Task<string?> StartAsync(Func<string, string> convert, string password, byte startLength = 0,
        byte length = byte.MaxValue, char[]? alphabet = null)
    {
        alphabet ??= Alphabet.English + Alphabet.Numbers + Alphabet.SpecialKeys;
        string? pw = null;


        var processorCount = Environment.ProcessorCount;
        var tasks = new Task[processorCount];


        for (var i = 0; i < processorCount; i++)
        {
            var i1 = i;
            tasks[i] = Task.Run(() => TryPassword(i1));
        }


        await Task.WhenAll(tasks);

        return pw;

        void TryPassword(int taskIndex)
        {
            var currentAttempt = new char[startLength + taskIndex];

            for (var i = 0; i < currentAttempt.Length; i++)
                currentAttempt[i] = alphabet[0];

            while (true)
            {
                if (pw != null)
                    break;


                var attempt = new string(currentAttempt);


                if (convert(attempt) == password)
                {
                    pw = attempt;
                    break;
                }


                var i = length - 1;
                currentAttempt[i] = alphabet[(Array.IndexOf(alphabet, currentAttempt[i]) + 1) % alphabet.Length];

                while (currentAttempt[i] == alphabet[0] && i > 0)
                {
                    if (pw != null)
                        break;
                    i--;
                    currentAttempt[i] = alphabet[(Array.IndexOf(alphabet, currentAttempt[i]) + 1) % alphabet.Length];
                }
            }
        }
    }
}