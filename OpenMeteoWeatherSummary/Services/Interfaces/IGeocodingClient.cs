using Models;
namespace Services.Interfaces;

public interface IGeocodingClient
{
    Task<LocationResult?> GetCoordinatesAsync(string city);
}