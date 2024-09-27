namespace Dalapagos.Tunneling.Cli.Services;

using Refit;

public interface IGetIp
{
    [Get("/ip")]
    Task<string> GetIpAsync(CancellationToken cancellationToken);
}