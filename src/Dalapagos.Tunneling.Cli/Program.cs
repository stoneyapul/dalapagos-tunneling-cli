using Dalapagos.Tunneling.Cli.Commands;
using Microsoft.Extensions.Hosting;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    Console.WriteLine("Canceling...");
    cts.Cancel();
    e.Cancel = true;
};

//args = ["add-device", "Test device1"];
await Host.CreateDefaultBuilder(args)
    .RunCommandLineApplicationAsync<AppCommand>(args, cts.Token);