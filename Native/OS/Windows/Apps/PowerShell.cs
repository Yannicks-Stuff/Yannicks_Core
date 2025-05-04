using System.Diagnostics;
using System.Text;

namespace Yannick.Native.OS.Windows.Apps;

public class PowerShell : IDisposable
{
    private readonly List<string> _puffer = new();
    private Process? _process;


    public PowerShell()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        _process = new Process { StartInfo = startInfo };
        _process.Start();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<string>> ReadAsync(CancellationToken token = default)
    {
        _puffer.Clear();

        while (!_process.StandardOutput.EndOfStream && !token.IsCancellationRequested)
        {
            var line = await _process.StandardOutput.ReadLineAsync(token);
            if (line != null)
                _puffer.Add(line);
        }

        return _puffer;
    }

    public async Task WriteAsync(string command, CancellationToken token = default)
    {
        await _process.StandardInput.WriteLineAsync(command);
        await _process.StandardInput.FlushAsync(token);
    }

    public IEnumerable<string> Read()
    {
        _puffer.Clear();

        while (_process.StandardOutput.ReadLine() is { } line)
        {
            _puffer.Add(line);
        }

        return _puffer;
    }

    public void Write(string command)
    {
        _process.StandardInput.WriteLine(command);
        _process.StandardInput.Flush();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing || _process == null) return;
        _process.Dispose();
        _process = null;
    }


    public static string[] WriteAndRead(string scriptText)
    {
        var startInfo = new ProcessStartInfo()
        {
            FileName = "powershell.exe",
            Arguments = $"-NoProfile -ExecutionPolicy Unrestricted -Command \"{scriptText}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(startInfo);
        using var reader = process.StandardOutput;
        return reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
    }
}