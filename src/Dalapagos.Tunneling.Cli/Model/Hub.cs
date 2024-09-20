namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class Hub
{
    [JsonPropertyName("hubId")]
    public Guid HubId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("connectedDevices")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ConnectedDevices { get; set; }

    [JsonPropertyName("totalDevices")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalDevices { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; } = default!;

    [JsonPropertyName("status")]
    public string Status { get; set; } = default!;

    [JsonPropertyName("devices")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<Device>? Devices { get; set; }
}
