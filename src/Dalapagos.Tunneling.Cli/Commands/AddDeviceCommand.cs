namespace Dalapagos.Tunneling.Cli.Commands;

using System.Text.Json;
using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Model;
using Services;

[Command(Description = "Add a new device.")]
internal sealed class AddDeviceCommand : CommandBase
{
    [Argument(0, Description = "The device name.")]
    public required string Name { get; set; }

    [Option(ShortName = "oid", Description = "An optional organization id.")]
    public string? OrganizationId { get; set; }

    [Option(ShortName = "hid", Description = "An optional hub id.")]
    public Guid? HubId { get; set; }

    [Option(ShortName = "os", Description = "An optional OS.")]
    public Os Os { get; set; }

    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(OrganizationId))
            {
                OrganizationId = ServiceClient.OrganizationId;
            }

            ArgumentException.ThrowIfNullOrWhiteSpace(OrganizationId, "OrganizationId");
            await EnsureAuthenticatedAsync(console, cancellationToken);
            
            var request = new AddDeviceRequest
            {
                Name = Name,
                Os = Os.ToString(),
                HubId = HubId
            };

            ConsoleHelper.WriteInfo(console, "Adding device...");
            Console.WriteLine();

            var retryPipeline = GetRetryPipeline();
            var response = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Devices.AddSDeviceAsync(OrganizationId, request, ct),
                cancellationToken);

            EnsureSuccess(console, response);

            var device = response.Device;
            if (device == null)
            {
                ConsoleHelper.WriteInfo(console, "Device not found.");
                return 1;
            }
           
            var output = JsonSerializer.Serialize(device, JsonIndented);
            return 0;
        }
        catch (Exception e)
        {
            ConsoleHelper.WriteError(console, e.Message);
            return 1;
         }
    }
}
