namespace Dalapagos.Tunneling.Cli.Services;

using Model;
using Refit;

public interface IDeviceService
{
    [Post("/v1/organizations/{organization_id}/devices")]
    Task<DeviceResponse> AddDeviceAsync(
        [AliasAs("organization_id")] string organizationId,
        AddDeviceRequest request, 
        CancellationToken cancellationToken
    );

    [Delete("/v1/organizations/{organization_id}/devices/{device_id}")]
    Task<ResponseBase> RemoveDeviceByIdAsync(
        [AliasAs("organization_id")] string organizationId,
        [AliasAs("device_id")] string deviceId, 
        CancellationToken cancellationToken
    );

    [Get("/v1/organizations/{organization_id}/devices/{device_id}/pairing-script")]
    Task<DevicePairingScriptResponse> GetDevicePairingScriptByIdAsync(
        [AliasAs("organization_id")] string organizationId, 
        [AliasAs("device_id")] string deviceId, 
        CancellationToken cancellationToken);
}