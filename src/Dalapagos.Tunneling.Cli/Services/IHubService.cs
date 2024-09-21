namespace Dalapagos.Tunneling.Cli.Services;

using Model;
using Refit;

public interface IHubService
{
    [Get("/v1/organizations/{organization_id}/hubs")]
    Task<GetHubsResponse> GetHubsByOrganizationIdAsync([AliasAs("organization_id")] string organizationId, CancellationToken cancellationToken);

    [Get("/v1/organizations/{organization_id}/hubs/{hub_id}")]
    Task<HubResponse> GetHubByIdAsync(
        [AliasAs("organization_id")] string organizationId, 
        [AliasAs("hub_id")] string hubId, 
        CancellationToken cancellationToken);

    [Post("/v1/organizations/{organization_id}/hubs")]
    Task<HubResponse> AddHubAsync(
        [AliasAs("organization_id")] string organizationId,
        AddHubRequest request, 
        CancellationToken cancellationToken
    );

    [Delete("/v1/organizations/{organization_id}/hubs/{hub_id}")]
    Task<ResponseBase> RemoveHubByIdAsync(
        [AliasAs("organization_id")] string organizationId,
        [AliasAs("hub_id")] string hubId, 
        CancellationToken cancellationToken
    );
}
