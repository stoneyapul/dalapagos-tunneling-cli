namespace Dalapagos.Tunneling.Cli.Commands;

using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Model;
using Services;

[Command(Description = "Create a tunnel.")]
internal sealed class AddTunnelCommand : CommandBase
{
    [Argument(0, Description = "The device id.")]
    public required string DeviceId { get; set; }

    [Option(ShortName = "oid", Description = "An optional organization id.")]
    public string? OrganizationId { get; set; }

    [Option(ShortName = "pro", Description = "An optional protocol.")]
    public Protocol Protocol { get; set; }

    [Option(Description = "An optional port.")]
    public ushort? Port { get; set; }

    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            OrganizationId = await EnsureOrganizationIdAsync(console, OrganizationId);
            await EnsureAuthenticatedAsync(console, cancellationToken);
            var retryPipeline = GetRetryPipeline();
           
            var ip = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Ip.GetIpAsync(ct),
                cancellationToken);

            var request = new AddTunnelRequest
            {
                DeviceId = Guid.Parse(DeviceId),
                Protocol = Protocol.ToString(),
                Port = Port,
                AllowedIp = ip,
                DeleteAfterMin = 60
            };

            ConsoleHelper.WriteInfo(console, $"Creating tunnel to {DeviceId}...");

             var response = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Tunnels.AddTunnelAsync(OrganizationId, request, ct),
                cancellationToken);

            EnsureSuccess(console, response);

            var tunnel = response.Tunnel;
            if (tunnel == null)
            {
                ConsoleHelper.WriteInfo(console, "Tunnel not found.");
                return 1;
            }

            Console.WriteLine(tunnel.Url);
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