namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class AddDeviceRequest
{
    [JsonPropertyName("hubId")]
    public Guid? HubId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("os")]
    public string Os { get; set; } = "Linux";
}