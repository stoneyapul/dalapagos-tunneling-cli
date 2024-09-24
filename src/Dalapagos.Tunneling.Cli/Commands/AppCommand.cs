namespace Dalapagos.Tunneling.Cli.Commands;

using McMaster.Extensions.CommandLineUtils;

[Command(Description = "Dalapagos Tunneling Commands")]
[Subcommand(typeof(GetOrgCommand))]
[Subcommand(typeof(GetOrgsCommand))]
[Subcommand(typeof(SetOrgCommand))]
[Subcommand(typeof(GetHubCommand))]
[Subcommand(typeof(GetHubsCommand))]
[Subcommand(typeof(AddHubCommand))]
[Subcommand(typeof(DelHubCommand))]
[Subcommand(typeof(AddDeviceCommand))]
internal class AppCommand
{
        public int OnExecute(IConsole console, CommandLineApplication app)
    {
        app.ShowHelp();
        return 0;
    }
}
