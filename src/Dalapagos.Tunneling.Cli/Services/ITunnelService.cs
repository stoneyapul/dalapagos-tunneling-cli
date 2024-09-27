namespace Dalapagos.Tunneling.Cli.Services;

using Model;
using Refit;

public interface ITunnelService
{
    [Post("/v1/organizations/{organization_id}/tunnels")]
    Task<TunnelResponse> AddTunnelAsync(
        [AliasAs("organization_id")] string organizationId,
        AddTunnelRequest request, 
        CancellationToken cancellationToken
    );
}
