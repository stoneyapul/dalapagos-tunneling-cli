namespace Dalapagos.Tunneling.Cli.Commands;

using System.Text.Json;
using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Services;


[Command(Description = "Get a list of organizations.")]
internal class GetOrgsCommand
{
    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            if (!await AuthenticationHelper.EnsureAuthenticatedAsync(console, cancellationToken))
            {
                ConsoleHelper.WriteError(console, "Access denied.");
                return 1;
            }
            
            ConsoleHelper.WriteInfo(console, "Getting organizations...");
            Console.WriteLine();

            var response = await ServiceClient.Organizations.GetOrganizationsByUserIdAsync(cancellationToken);
            var organizations = response.Organizations;
            if (organizations == null || organizations.Length == 0)
            {
                ConsoleHelper.WriteError(console, "No organizations found.");
                return 1;
            }

            var output = JsonSerializer.Serialize(organizations, new JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine(output);
            Console.WriteLine();

            return 0;
        }
        catch (Exception e)
        {
            ConsoleHelper.WriteError(console, e.Message);
            return 1;
         }
    }
}
