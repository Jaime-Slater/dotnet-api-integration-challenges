using System.Net.Http.Json;
using Models;
using Services.Interfaces;

namespace Services;

public class OpenMeteoClient : IOpenMeteoClient
{
    private readonly HttpClient _httpClient;

    public OpenMeteoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DailyForecast> GetForecastAsync(double latitude, double longitude)
    {
        var url =
            $"forecast?latitude={latitude}&longitude={longitude}" +
            "&daily=temperature_2m_max,temperature_2m_min,precipitation_sum" +
            "&timezone=Europe%2FLondon";

        using var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();

            throw new HttpRequestException(
                $"Forecast API failed. Status: {(int)response.StatusCode}. Body: {body}");
        }

        var content = await response.Content.ReadFromJsonAsync<ForecastResponse>();

        return content?.Daily ?? new DailyForecast();
    }
}