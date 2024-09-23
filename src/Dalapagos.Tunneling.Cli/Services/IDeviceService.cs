namespace Dalapagos.Tunneling.Cli.Services;

using Model;
using Refit;

public interface IDeviceService
{
    [Post("/v1/organizations/{organization_id}/devices")]
    Task<DeviceResponse> AddSDeviceAsync(
        [AliasAs("organization_id")] string organizationId,
        AddDeviceRequest request, 
        CancellationToken cancellationToken
    );

}
