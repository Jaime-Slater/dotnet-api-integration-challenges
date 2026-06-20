using Handlers.Interfaces;
using Models;

namespace Handlers;

public sealed class WeatherDataHandler : IWeatherDataHandler
{
    public List<WeatherDaySummary> ProcessForecast(DailyForecast dailyForecast)
    {
        if (!ValidateArrayCounts(dailyForecast))
            return [];

        var summary = new List<WeatherDaySummary>();

        for (var i = 0; i < dailyForecast.Time.Count; i++)
        {
            var minTemp = dailyForecast.TemperatureMin[i];
            var maxTemp = dailyForecast.TemperatureMax[i];
            var rainfall = dailyForecast.PrecipitationSum[i];
            var averageTemp = (minTemp + maxTemp) / 2;

            summary.Add(new WeatherDaySummary
            {
                Date = dailyForecast.Time[i],
                MinTemperature = minTemp,
                MaxTemperature = maxTemp,
                AverageTemperature = averageTemp,
                Rainfall = rainfall,
                Risk = DetermineRisk(maxTemp, rainfall)
            });
        }

        return summary;
    }

    private bool ValidateArrayCounts(DailyForecast dailyForecast)
    {
        var count = dailyForecast.Time.Count;

        return count == dailyForecast.TemperatureMax.Count
            && count == dailyForecast.TemperatureMin.Count
            && count == dailyForecast.PrecipitationSum.Count;
    }

    private string DetermineRisk(decimal maxTemp, decimal rainfall)
    {
        if (rainfall >= 10)
            return "High Rain Risk";

        if (rainfall >= 2)
            return "Rain Risk";

        if (maxTemp <= 5)
            return "Cold Risk";

        return "Normal";
    }
}