namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class AddHubRequest
{
    [JsonPropertyName("hubId")]
    public Guid? HubId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("location")]
    public required string Location { get; set; }
}
