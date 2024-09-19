namespace Dalapagos.Tunneling.Cli.Services;

using System.Net.Http.Headers;
using Helpers;
using Refit;

public class ServiceClient()
{
   private const string BaseUrl = "https://dalapagos-tunneling.salmonstone-ca93e2e0.westus3.azurecontainerapps.io";

    public static IOrganizationService Organizations 
    { 
        get
        {
            var httpClient = CreateHttpClient(BaseUrl, AuthenticationHelper.AccessToken);
            return RestService.For<IOrganizationService>(httpClient);
        } 
    }

    private static HttpClient CreateHttpClient(string baseUrl, string? accessToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(accessToken, nameof(accessToken));

        var refitSettings = new RefitSettings { ContentSerializer = new SystemTextJsonContentSerializer() };
        var httpClient = RestService.CreateHttpClient(baseUrl, refitSettings);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        return httpClient;
    }
}
