namespace Dalapagos.Tunneling.Cli.Services;

using System.Net.Http.Headers;
using Helpers;
using Refit;

internal static class ServiceClient
{
    private const string BaseUrl = "https://dalapagos-tunneling.salmonstone-ca93e2e0.westus3.azurecontainerapps.io";
    private const string IpBaseUrl = "https://ipapi.co";

    public static string? OrganizationId { get; set; }

    public static string OrganizationIdPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".dlpgs_org_id");

    public static async Task SaveOrganizationIdAsync(string token)
    {
        await File.WriteAllTextAsync(OrganizationIdPath, token);
    }

    public static async Task<string?> ReadOrganizationIdAsync()
    {
        if (File.Exists(OrganizationIdPath))
        {
            return await File.ReadAllTextAsync(OrganizationIdPath);
        }

        return null;
    }

    public static IOrganizationService Organizations 
    { 
        get
        {
            var httpClient = CreateHttpClient(BaseUrl, AuthenticationHelper.AccessToken);
            return RestService.For<IOrganizationService>(httpClient);
        } 
    }

    public static IHubService Hubs 
    { 
        get
        {
            var httpClient = CreateHttpClient(BaseUrl, AuthenticationHelper.AccessToken);
            return RestService.For<IHubService>(httpClient);
        } 
    }

    public static IDeviceService Devices 
    { 
        get
        {
            var httpClient = CreateHttpClient(BaseUrl, AuthenticationHelper.AccessToken);
            return RestService.For<IDeviceService>(httpClient);
        } 
    }

    public static ITunnelService Tunnels 
    { 
        get
        {
            var httpClient = CreateHttpClient(BaseUrl, AuthenticationHelper.AccessToken);
            return RestService.For<ITunnelService>(httpClient);
        } 
    }

    public static IGetIp Ip 
    { 
        get
        {
            var httpClient = CreateHttpClient(IpBaseUrl);
            return RestService.For<IGetIp>(httpClient);
        } 
    }

    private static HttpClient CreateHttpClient(string baseUrl, string? accessToken = null)
    {
        var refitSettings = new RefitSettings { ContentSerializer = new SystemTextJsonContentSerializer() };
        var httpClient = RestService.CreateHttpClient(baseUrl, refitSettings);

        if (accessToken != null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
 
        return httpClient;
    }
}
