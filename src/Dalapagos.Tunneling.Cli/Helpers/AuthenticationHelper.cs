namespace Dalapagos.Tunneling.Cli.Helpers;

using System.IdentityModel.Tokens.Jwt;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Identity.Client;

internal static class AuthenticationHelper
{
    public static string? AccessToken { get; private set; }

    private const string tenantId = "13cfba4f-9203-4496-a424-b19fe4f06252";
    private const string clientId = "5d91e02e-552b-4968-aea4-d153fcd116a1";
    private const string scope = "api://dalapagos-tunneling-api/.default";

    public static async Task<bool> EnsureAuthenticatedAsync(IConsole console, CancellationToken cancellationToken)
    {
        try
        {
            if (AccessToken != null && CheckTokenExpirationIsValid(AccessToken))
            {
                return true;
            }
            
            var publicMsalClient = PublicClientApplicationBuilder.CreateWithApplicationOptions(new ())
                .WithTenantId(tenantId)
                .WithClientId(clientId)
                .Build();

            AuthenticationResult? msalAuthenticationResult = null;

            // Attempt to use a cached access token if one is available. This will renew existing, but
            // expired access tokens if possible. In this specific sample, this will always result in
            // a cache miss, but this pattern would be what you'd use on subsequent calls that require
            // the usage of the same access token.
            var accounts = (await publicMsalClient.GetAccountsAsync()).ToList();
 
            if (accounts.Count > 1)
            {
                try
                {
                    msalAuthenticationResult = await publicMsalClient.AcquireTokenSilent([scope], accounts.First()).ExecuteAsync(cancellationToken);
                }
                catch (MsalUiRequiredException)
                {
                    // No usable cached token was found for this scope + account or Azure AD insists in
                    // an interactive user flow.
                }
            }        
            
            // Initiate the device code flow.
            msalAuthenticationResult ??= await publicMsalClient.AcquireTokenWithDeviceCode([scope], deviceCodeResultCallback =>
            {
                // This will print the message on the console which tells the user where to go sign-in using
                // a separate browser and the code to enter once they sign in.
                // The AcquireTokenWithDeviceCode() method will poll the server after firing this
                // device code callback to look for the successful login of the user via that browser.
                Console.WriteLine();
                ConsoleHelper.WriteInfo(console, deviceCodeResultCallback.Message);
                
                return Task.CompletedTask;
            }).ExecuteAsync();

            AccessToken = msalAuthenticationResult.AccessToken;
            return true;
        }
        catch (Exception e)
        {
            ConsoleHelper.WriteError(console, e.Message);
            AccessToken = null;
            return false;
         }
    }

    private static long GetTokenExpirationTime(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
        var ticks= long.Parse(tokenExp);

        return ticks;
    }

    private static bool CheckTokenExpirationIsValid(string token)
    {
        var tokenTicks = GetTokenExpirationTime(token);
        var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;
        var now = DateTime.Now.ToUniversalTime();

        return  tokenDate >= now;
   }
}