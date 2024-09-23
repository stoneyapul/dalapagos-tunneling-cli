namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class DeviceResponse : ResponseBase
{
    [JsonPropertyName("data")]
    public required Device Device { get; set; }
}
