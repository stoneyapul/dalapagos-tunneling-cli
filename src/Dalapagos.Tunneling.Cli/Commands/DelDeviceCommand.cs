namespace Dalapagos.Tunneling.Cli.Commands;

using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Services;

[Command(Description = "Delete a device.")]
internal sealed class DelDeviceCommand : CommandBase
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
            
            ConsoleHelper.WriteInfo(console, $"Deleting device {DeviceId}...");

            var retryPipeline = GetRetryPipeline();
            var response = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Devices.RemoveDeviceByIdAsync(OrganizationId, DeviceId, ct),
                cancellationToken);

            EnsureSuccess(console, response);

            ConsoleHelper.WriteInfo(console, "Don't forget to remove the tunneling client on the device...");
            return 0;
        }
        catch (Exception e)
        {
            ConsoleHelper.WriteError(console, e.Message);
            return 1;
         }
    }
}
