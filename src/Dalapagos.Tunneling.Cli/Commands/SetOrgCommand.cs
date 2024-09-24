namespace Dalapagos.Tunneling.Cli.Commands;

using Helpers;
using McMaster.Extensions.CommandLineUtils;

[Command(Description = "Set the default organization.")]
internal sealed class SetOrgCommand : CommandBase
{
    [Argument(0, Description = "The organization id.")]
    public required string OrganizationId { get; set; }

    public async Task<int> OnExecuteAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            await EnsureAuthenticatedAsync(console, cancellationToken);

            if (!Guid.TryParse(OrganizationId, out _))
            {
                ConsoleHelper.WriteError(console, "Invalid organization id.");
                return 1;
            }

            await UseOrganizationAsync(console, OrganizationId);
  
            return 0;
        }
        catch (Exception e)
        {
            ConsoleHelper.WriteError(console, e.Message);
            return 1;
         }
    }
}
