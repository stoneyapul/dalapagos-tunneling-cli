namespace Dalapagos.Tunneling.Cli.Commands;

using System.Text.Json;
using Helpers;
using McMaster.Extensions.CommandLineUtils;
using Model;
using Polly;
using Polly.Retry;
using Services;

internal abstract class CommandBase
{
    private const int RetryAttempts = 2;

    protected static JsonSerializerOptions JsonIndented { get; } = new JsonSerializerOptions { WriteIndented = true };

    protected virtual async Task EnsureAuthenticatedAsync(IConsole console, CancellationToken cancellationToken)
    {
        if (!await AuthenticationHelper.EnsureAuthenticatedAsync(console, cancellationToken))
        {
            throw new Exception("Access denied.");
        }
    }

    protected virtual void EnsureSuccess(IConsole console, ResponseBase response)
    {
        if (!response.IsSuccessful)
        {
            if (response.Errors != null && response.Errors.Length > 0)
            {
                foreach (var error in response.Errors)
                {
                    ConsoleHelper.WriteError(console, error);
                }
            }

            throw new Exception("An error occurred.");
        }
    }

    protected virtual void UseOrganization(IConsole console, string organizationId)
    {
        ConsoleHelper.WriteInfo(console, $"Using organization: {organizationId}");
        ServiceClient.OrganizationId = organizationId;
    }

    protected ResiliencePipeline GetRetryPipeline()
    {
        return new ResiliencePipelineBuilder()
            .AddRetry(
                new RetryStrategyOptions
                {
                    BackoffType = DelayBackoffType.Exponential,
                    MaxRetryAttempts = RetryAttempts,
                    UseJitter = true
                }
            )
            .Build();
    }
}
