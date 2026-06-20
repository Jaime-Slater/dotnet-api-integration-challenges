using Models;

namespace Handlers.Interfaces;

public interface IWeatherDataHandler
{
    List<WeatherDaySummary> ProcessForecast(DailyForecast dailyForecast);
}