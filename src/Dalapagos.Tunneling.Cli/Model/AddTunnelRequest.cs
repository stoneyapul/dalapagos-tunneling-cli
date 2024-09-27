namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class AddTunnelRequest
{
    [JsonPropertyName("deviceId")]
    public Guid DeviceId { get; set; }

    [JsonPropertyName("protocol")]
    public string Protocol { get; set; } = default!;

    [JsonPropertyName("port")]
    public ushort? Port { get; set; }

    [JsonPropertyName("allowedIp")]
    public string? AllowedIp { get; set; }

    [JsonPropertyName("deleteAfterMin")]
    public int DeleteAfterMin { get; set; } = 60;
}
