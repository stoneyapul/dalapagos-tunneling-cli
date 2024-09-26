namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class DevicePairingScript
{
    [JsonPropertyName("pairingScript")]
    public string? PairingScript { get; set; }
}
