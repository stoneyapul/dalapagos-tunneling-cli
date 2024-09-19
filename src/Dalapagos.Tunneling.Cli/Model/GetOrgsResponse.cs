namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class GetOrgsResponse : ResponseBase
{
    [JsonPropertyName("data")]
    public required Organization[] Organizations { get; set; }
}
