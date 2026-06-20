using System.Net.Http.Json;
using Models;
using Services.Interfaces;

namespace Services;

public class GeocodingClient : IGeocodingClient
{
    private readonly HttpClient _httpClient;
    public GeocodingClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LocationResult?> GetCoordinatesAsync(string city)
    {
        var encodedCity = Uri.EscapeDataString(city);

        using var response = await _httpClient.GetAsync(
            $"search?name={encodedCity}&count=1&language=en&format=json");

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();

            throw new HttpRequestException(
                $"Geocoding API failed. Status: {(int)response.StatusCode}. Body: {body}");
        }

        var content = await response.Content.ReadFromJsonAsync<GeoCodingResponse>();

        return content?.Results.FirstOrDefault();
    }
}