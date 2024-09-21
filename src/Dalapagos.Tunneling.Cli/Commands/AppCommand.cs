namespace Dalapagos.Tunneling.Cli.Commands;

using McMaster.Extensions.CommandLineUtils;

[Command(Description = "Dalapagos Tunneling Commands")]
[Subcommand(typeof(GetOrgCommand))]
[Subcommand(typeof(GetOrgsCommand))]
[Subcommand(typeof(SetOrgCommand))]
[Subcommand(typeof(GetHubCommand))]
[Subcommand(typeof(GetHubsCommand))]
[Subcommand(typeof(AddHubCommand))]
[Subcommand(typeof(RemoveHubCommand))]
internal class AppCommand
{
    private static readonly string[] quit = ["exit", "quit", "q"];

    public async Task<int> OnExecuteAsync(IConsole console, CommandLineApplication app)
    {
        console.WriteLine("You must specify a command");
        app.ShowHelp();
        while (true)
        {
            var command = Prompt.GetString("Enter command:", promptColor: ConsoleColor.Blue);

            if (quit.Contains(command))
            {
                break;
            }

            if (command != null)
                await app.ExecuteAsync(command.Split(' '));
            else
                console.WriteLine("Please enter a valid command");
        }

        return 1;
    }

}
