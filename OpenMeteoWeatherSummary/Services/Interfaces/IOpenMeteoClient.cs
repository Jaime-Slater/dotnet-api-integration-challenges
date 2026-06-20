using Models;

namespace Services.Interfaces;

public interface IOpenMeteoClient
{
    Task<DailyForecast> GetForecastAsync(double latitude, double longitude);
}