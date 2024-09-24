namespace Dalapagos.Tunneling.Cli.Commands;

using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Services;

[Command(Description = "Get the default organization.")]
internal sealed class GetOrgCommand : CommandBase
{
    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            await EnsureAuthenticatedAsync(console, cancellationToken);

            if (!string.IsNullOrWhiteSpace(ServiceClient.OrganizationId))
            {
                ConsoleHelper.WriteInfo(console, ServiceClient.OrganizationId);
                return 0;
            }

            ConsoleHelper.WriteInfo(console, "Getting organization...");
            Console.WriteLine();

            var retryPipeline = GetRetryPipeline();
            var response = await retryPipeline.ExecuteAsync(
                async (ct) => await ServiceClient.Organizations.GetOrganizationsByUserIdAsync(ct),
                cancellationToken);

            EnsureSuccess(console, response);

            var organizations = response.Organizations;
            if (organizations == null || organizations.Length == 0)
            {
                ConsoleHelper.WriteError(console, "No organizations found.");
                return 1;
            }

            ServiceClient.OrganizationId = organizations[0].OrganizationId.ToString();
            ConsoleHelper.WriteInfo(console, ServiceClient.OrganizationId);
  
            return 0;
        }
        catch (Exception e)
        {
            ConsoleHelper.WriteError(console, e.Message);
            return 1;
         }
    }
}
