namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class GetHubsResponse : ResponseBase
{
    [JsonPropertyName("data")]
    public required Hub[] Hubs { get; set; }
}
