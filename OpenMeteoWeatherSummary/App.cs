using Handlers;
using Handlers.Interfaces;
using Services;
using Services.Interfaces;

public sealed class App
{

    private readonly IGeocodingClient _geocodingClient;
    private readonly IOpenMeteoClient _openMeteoClient;
    private readonly IWeatherDataHandler _weatherDataHandler;

    public App(IGeocodingClient geocodingClient, IOpenMeteoClient openMeteoClient, IWeatherDataHandler weatherDataHandler)
    {
        _geocodingClient = geocodingClient;
        _openMeteoClient = openMeteoClient;
        _weatherDataHandler = weatherDataHandler;
    }

    public async Task RunAsync(string[] args)
    {
        var city = args.Length > 0 ? args[0] : "Chesterfield";

        Console.WriteLine($"Getting coordinates for city: {city}");

        var locationResult = await _geocodingClient.GetCoordinatesAsync(city);

        if (locationResult is null)
        {
            Console.WriteLine($"No results found for city: {city}");
            return;
        }

        Console.WriteLine(
            $"City: {locationResult.Name}, Country: {locationResult.Country}, Latitude: {locationResult.Latitude}, Longitude: {locationResult.Longitude}");

        var dailyForecast = await _openMeteoClient.GetForecastAsync(
            locationResult.Latitude,
            locationResult.Longitude);

        var forecastSummary = _weatherDataHandler.ProcessForecast(dailyForecast);

        if (forecastSummary.Count == 0)
        {
            Console.WriteLine("No forecast data available.");
            return;
        }

        foreach (var summary in forecastSummary)
        {
            Console.WriteLine(
                $"Date: {summary.Date:yyyy-MM-dd}, " +
                $"Min Temp: {summary.MinTemperature:F1}°C, " +
                $"Max Temp: {summary.MaxTemperature:F1}°C, " +
                $"Average Temp: {summary.AverageTemperature:F1}°C, " +
                $"Rainfall: {summary.Rainfall:F1}mm, " +
                $"Risk: {summary.Risk}");
        }

        var wettestDay = forecastSummary.OrderByDescending(x => x.Rainfall).First();
        var coldestDay = forecastSummary.OrderBy(x => x.AverageTemperature).First();
        var averageDailyTemperature = forecastSummary.Average(x => x.AverageTemperature);
        var totalRainfall = forecastSummary.Sum(x => x.Rainfall);

        Console.WriteLine();
        Console.WriteLine("7-Day Summary");
        Console.WriteLine($"Average Daily Temperature: {averageDailyTemperature:F1}°C");
        Console.WriteLine($"Total Rainfall: {totalRainfall:F1}mm");
        Console.WriteLine($"Wettest Day: {wettestDay.Date:yyyy-MM-dd} - {wettestDay.Rainfall:F1}mm");
        Console.WriteLine($"Coldest Day: {coldestDay.Date:yyyy-MM-dd} - {coldestDay.AverageTemperature:F1}°C average");
    }
}
