namespace Dalapagos.Tunneling.Cli.Commands;

using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Services;

[Command(Description = "De-provision a hub.")]
internal sealed class DelHubCommand : CommandBase
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
            
            ConsoleHelper.WriteInfo(console, $"De-provisioning hub {HubId}...");
            Console.WriteLine();

            var retryPipeline = GetRetryPipeline();
            var response = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Hubs.RemoveHubByIdAsync(OrganizationId, HubId, ct),
                cancellationToken);

            EnsureSuccess(console, response);

            ConsoleHelper.WriteInfo(console, "Hub is de-provisioning. This takes a few minutes...");
            return 0;
        }
        catch (Exception e)
        {
            ConsoleHelper.WriteError(console, e.Message);
            return 1;
         }
    }
}
