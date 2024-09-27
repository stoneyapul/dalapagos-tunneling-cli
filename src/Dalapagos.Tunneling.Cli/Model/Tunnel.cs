namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class Tunnel
{
    [JsonPropertyName("protocol")]
    public required string Protocol { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("allowed-ips")]
    public string[]? AllowedIps { get; set; }
}
