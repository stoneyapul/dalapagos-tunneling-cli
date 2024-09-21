namespace Dalapagos.Tunneling.Cli.Commands;

using System.Text.Json;
using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Services;

[Command(Description = "Get a list of organizations.")]
internal sealed class GetOrgsCommand : CommandBase
{
    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            await EnsureAuthenticatedAsync(console, cancellationToken);
            
            ConsoleHelper.WriteInfo(console, "Getting organizations...");
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
           
            var output = JsonSerializer.Serialize(organizations, JsonIndented);

            Console.WriteLine(output);
            Console.WriteLine();

            UseOrganization(console, organizations[0].OrganizationId.ToString());
  
            return 0;
        }
        catch (Exception e)
        {
            ConsoleHelper.WriteError(console, e.Message);
            return 1;
         }
    }
}
