namespace Dalapagos.Tunneling.Cli.Model;

using System.Text.Json.Serialization;

public class Organization
{
    [JsonPropertyName("organizationId")]
    public Guid OrganizationId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
