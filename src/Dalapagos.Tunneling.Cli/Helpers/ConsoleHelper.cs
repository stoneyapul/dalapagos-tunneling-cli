namespace Dalapagos.Tunneling.Cli.Helpers;

using McMaster.Extensions.CommandLineUtils;

internal static class ConsoleHelper
{
    public static void WriteInfo(IConsole console, Stream message)
    {
        using var reader = new StreamReader(message);
        WriteInfo(console, reader.ReadToEnd());
    }

    public static void WriteInfo(IConsole console, string message)
    {
        console.ForegroundColor = ConsoleColor.Blue;
        console.WriteLine(message);
        console.ResetColor();
    }

    public static void WriteError(IConsole console, string message)
    {
        console.ForegroundColor = ConsoleColor.Red;
        console.WriteLine(message);
        console.ResetColor();
    }
}