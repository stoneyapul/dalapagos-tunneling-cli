namespace Dalapagos.Tunneling.Cli.Commands;

using System.Text.Json;
using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Services;

[Command(Description = "Get a list of hubs.")]
internal sealed class GetHubCommand : CommandBase
{
    [Argument(0, Description = "The hub id.")]
    public required string HubId { get; set; }

    [Option(ShortName = "oid", Description = "An optional organization id.")]
    public string? OrganizationId { get; set; }

    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            OrganizationId = await EnsureOrganizationIdAsync(console, OrganizationId);
            await EnsureAuthenticatedAsync(console, cancellationToken);
            
            ConsoleHelper.WriteInfo(console, $"Getting hub {HubId}...");
            Console.WriteLine();

            var retryPipeline = GetRetryPipeline();
            var response = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Hubs.GetHubByIdAsync(OrganizationId, HubId, ct),
                cancellationToken);

            EnsureSuccess(console, response);

            var hub = response.Hub;
            if (hub == null)
            {
                ConsoleHelper.WriteInfo(console, "Hub not found.");
                return 1;
            }
           
            var output = JsonSerializer.Serialize(hub, JsonIndented);

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