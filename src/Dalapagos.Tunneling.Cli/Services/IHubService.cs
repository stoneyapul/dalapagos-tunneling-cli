namespace Dalapagos.Tunneling.Cli.Services;

using Model;
using Refit;

public interface IHubService
{
    [Get("/v1/organizations/{organization_id}/hubs")]
    Task<GetHubsResponse> GetHubsByOrganizationIdAsync([AliasAs("organization_id")] string organizationId, CancellationToken cancellationToken);

}
