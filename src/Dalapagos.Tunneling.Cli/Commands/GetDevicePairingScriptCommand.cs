namespace Dalapagos.Tunneling.Cli.Commands;

using System.Text.Json;
using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Services;

[Command(Description = "Get a device pairing script.")]
internal sealed class GetDevicePairingScriptCommand : CommandBase
{
    [Argument(0, Description = "The device id.")]
    public required string DeviceId { get; set; }

    [Option(ShortName = "oid", Description = "An optional organization id.")]
    public string? OrganizationId { get; set; }

    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            OrganizationId = await EnsureOrganizationIdAsync(console, OrganizationId);
            await EnsureAuthenticatedAsync(console, cancellationToken);
            
            ConsoleHelper.WriteInfo(console, $"Getting device {DeviceId} pairing script...");

            var retryPipeline = GetRetryPipeline();
            var response = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Devices.GetDevicePairingScriptByIdAsync(OrganizationId, DeviceId, ct),
                cancellationToken);

            EnsureSuccess(console, response);

            var script = response.DevicePairingScript;
            if (script == null)
            {
                ConsoleHelper.WriteInfo(console, "Device pairing script not found.");
                return 1;
            }
           
            Console.WriteLine(script.PairingScript);
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
