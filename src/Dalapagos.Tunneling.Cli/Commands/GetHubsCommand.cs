namespace Dalapagos.Tunneling.Cli.Commands;

using System.Text.Json;
using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Services;

[Command(Description = "Get a list of hubs.")]
internal sealed class GetHubsCommand: CommandBase
{
    [Option(ShortName = "oid", Description = "An optional organization id.")]
    public string? OrganizationId { get; set; }

    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            OrganizationId = await EnsureOrganizationIdAsync(console, OrganizationId);
            await EnsureAuthenticatedAsync(console, cancellationToken);
            
            ConsoleHelper.WriteInfo(console, "Getting hubs...");
            Console.WriteLine();

            var retryPipeline = GetRetryPipeline();
            var response = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Hubs.GetHubsByOrganizationIdAsync(OrganizationId, ct),
                cancellationToken);

            EnsureSuccess(console, response);

            var hubs = response.Hubs;
            if (hubs == null || hubs.Length == 0)
            {
                ConsoleHelper.WriteInfo(console, "No hubs found.");
                return 1;
            }
           
            var output = JsonSerializer.Serialize(hubs, JsonIndented);

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
