using System.Text.Json.Serialization;

namespace Models;

public class DailyForecast
{
    public List<DateOnly> Time { get; set; } = [];

    [JsonPropertyName("temperature_2m_max")]
    public List<decimal> TemperatureMax { get; set; } = [];

    [JsonPropertyName("temperature_2m_min")]
    public List<decimal> TemperatureMin { get; set; } = [];

    [JsonPropertyName("precipitation_sum")]
    public List<decimal> PrecipitationSum { get; set; } = [];
}