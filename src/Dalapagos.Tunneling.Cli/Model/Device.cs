namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class Device
{
    [JsonPropertyName("deviceId")]
    public Guid DeviceId { get; set; }

    [JsonPropertyName("hubId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? HubId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("os")]
    public Os Os { get; set; }

    [JsonIgnore]
    public string? PairingScript { get; set; }
}
