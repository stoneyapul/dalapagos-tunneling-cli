namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class AddHubRequest
{
    [JsonPropertyName("hubId")]
    public Guid? HubId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("location")]
    public string Location { get; set; } = default!;

}
