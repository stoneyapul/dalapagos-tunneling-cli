namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class TunnelResponse : ResponseBase
{
    [JsonPropertyName("data")]
    public required Tunnel Tunnel { get; set; }
}
