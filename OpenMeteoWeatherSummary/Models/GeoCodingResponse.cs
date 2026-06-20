using System.Text.Json.Serialization;

namespace Models;

public class GeoCodingResponse
{
    [JsonPropertyName("results")]
    public List<LocationResult> Results { get; set; } = [];
}