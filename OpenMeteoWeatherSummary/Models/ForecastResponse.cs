using System.Text.Json.Serialization;

namespace Models;

public class ForecastResponse
{
    public DailyForecast Daily { get; set; } = new DailyForecast();

}