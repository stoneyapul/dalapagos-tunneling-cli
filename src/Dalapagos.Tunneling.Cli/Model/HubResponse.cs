namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class HubResponse : ResponseBase
{
    [JsonPropertyName("data")]
    public required Hub Hub { get; set; }
}
