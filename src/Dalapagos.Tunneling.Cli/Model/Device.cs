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

    [JsonIgnore]
    public Os Os { get; set; }

    [JsonPropertyName("os")]
    public string OsString => Os.ToString();

    [JsonIgnore]
    public string? PairingScript { get; set; }
}
