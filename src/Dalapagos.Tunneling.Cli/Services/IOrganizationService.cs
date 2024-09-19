namespace Dalapagos.Tunneling.Cli.Services;

using Model;
using Refit;

public interface IOrganizationService
{
    [Get("/v1/organizations")]
    Task<GetOrgsResponse> GetOrganizationsByUserIdAsync(CancellationToken cancellationToken);
}
