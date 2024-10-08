namespace Dalapagos.Tunneling.Cli.Commands;

using System.Text.Json;
using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Model;
using Services;

[Command(Description = "Provision a new hub.")]
internal sealed class AddHubCommand : CommandBase
{
    [Argument(0, Description = "The hub name.")]
    public required string Name { get; set; }

    [Argument(1, Description = "The hub location.")]
    public required ServerLocation Location { get; set; } = ServerLocation.West;

    [Option(ShortName = "oid", Description = "An optional organization id.")]
    public string? OrganizationId { get; set; }

    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            OrganizationId = await EnsureOrganizationIdAsync(console, OrganizationId);
            await EnsureAuthenticatedAsync(console, cancellationToken);
            
            var request = new AddHubRequest
            {
                Name = Name,
                Location = Location.ToString()
            };

            ConsoleHelper.WriteInfo(console, "Provisioning hub...");
            Console.WriteLine();

            var retryPipeline = GetRetryPipeline();
            var response = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Hubs.AddHubAsync(OrganizationId, request, ct),
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
