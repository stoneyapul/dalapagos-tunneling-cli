namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class DevicePairingScriptResponse : ResponseBase
{
    [JsonPropertyName("data")]
    public required DevicePairingScript DevicePairingScript { get; set; }
}
