# OpenMeteo Weather Summary

A small .NET console application built as an interview-style API integration challenge.

The application consumes the OpenMeteo geocoding and weather forecast APIs, maps the response data into C# models, applies simple business rules, and prints a 7-day weather summary to the console.

## Purpose

This project demonstrates practical C#/.NET API integration skills, including:

- Calling external REST APIs
- Using HttpClient with dependency injection
- Using IHttpClientFactory via typed clients
- Deserialising JSON into DTOs
- Mapping external API data into internal models
- Validating array-based API response data
- Applying business rules to API data
- Producing clean console output

## What the Application Does

The app accepts a city name from the command line.

If no city is supplied, it defaults to:

```
Chesterfield
```

The application then:

1. Calls the OpenMeteo geocoding API to find the city latitude and longitude
2. Calls the OpenMeteo forecast API using those coordinates
3. Retrieves daily forecast data for temperature and rainfall
4. Maps the forecast arrays into a list of daily summaries
5. Calculates the average temperature for each day
6. Assigns a simple weather risk status
7. Prints a 7-day summary to the console

## APIs Used

### OpenMeteo Geocoding API

Used to convert a city name into latitude and longitude.

Base URL:

```
https://geocoding-api.open-meteo.com/v1/
```

Endpoint used:

```
GET /search
```

Example:

```
https://geocoding-api.open-meteo.com/v1/search?name=Chesterfield&count=1&language=en&format=json
```

### OpenMeteo Forecast API

Used to retrieve daily weather forecast data.

Base URL:

```
https://api.open-meteo.com/v1/
```

Endpoint used:

```
GET /forecast
```

Daily values requested:

- Maximum temperature
- Minimum temperature
- Precipitation sum

Example:

```
https://api.open-meteo.com/v1/forecast?latitude=53.235&longitude=-1.421&daily=temperature_2m_max,temperature_2m_min,precipitation_sum&timezone=Europe%2FLondon
```

No API key or special headers are required.

## Weather Risk Rules

Each forecast day is assigned a weather risk.

The rules are checked in this order:

| Condition                           | Risk           |
| ----------------------------------- | -------------- |
| Rainfall is 10mm or higher          | High Rain Risk |
| Rainfall is 2mm or higher           | Rain Risk      |
| Maximum temperature is 5°C or lower | Cold Risk      |
| Anything else                       | Normal         |

Rainfall is checked before cold weather. This means a cold and very wet day is classified as a rain risk first.

## Tech Stack

- C#
- .NET 9
- Console application
- Microsoft.Extensions.Hosting
- Microsoft.Extensions.Http
- HttpClient
- IHttpClientFactory
- System.Net.Http.Json

## Project Structure

```
OpenMeteoWeatherSummary/
  App.cs
  Program.cs
  WeatherChallenge.csproj
  README.md

  Handlers/
    WeatherDataHandler.cs

    Interfaces/
      IWeatherDataHandler.cs

  Models/
    DailyForecast.cs
    ForecastResponse.cs
    GeoCodingResponse.cs
    LocationResult.cs
    WeatherDaySummary.cs

  Services/
    GeocodingClient.cs
    OpenMeteoClient.cs

    Interfaces/
      IGeocodingClient.cs
      IOpenMeteoClient.cs
```

## How to Run

From the OpenMeteoWeatherSummary folder, run:

```
dotnet restore
dotnet run
```

This will use the default city:

```
Chesterfield
```

To run the app for a different city, pass the city name as an argument:

```
dotnet run London
```

For city names with spaces, wrap the value in quotes:

```
dotnet run "New York"
```

## Example Output

```
Getting coordinates for city: Chesterfield
City: Chesterfield, Country: United Kingdom, Latitude: 53.235, Longitude: -1.421

Date: 2026-06-20, Min Temp: 12.1°C, Max Temp: 20.4°C, Average Temp: 16.3°C, Rainfall: 1.2mm, Risk: Normal
Date: 2026-06-21, Min Temp: 11.8°C, Max Temp: 18.2°C, Average Temp: 15.0°C, Rainfall: 3.4mm, Risk: Rain Risk

7-Day Summary
Average Daily Temperature: 15.8°C
Total Rainfall: 18.4mm
Wettest Day: 2026-06-21 - 3.4mm
Coldest Day: 2026-06-20 - 16.3°C average
```

Actual output will vary depending on the current forecast returned by the API.

## Key Implementation Details

The project uses the .NET Generic Host so the console app can use dependency injection in a similar style to an ASP.NET Core application.

Typed HttpClient registrations are configured in Program.cs:

- IGeocodingClient -> GeocodingClient
- IOpenMeteoClient -> OpenMeteoClient

The main application flow is handled by App.cs.

WeatherDataHandler is responsible for transforming the raw daily forecast data into internal WeatherDaySummary models.

The forecast API returns daily values as parallel arrays. The handler validates that these arrays have matching counts before processing them.

## Error Handling

The app handles these basic failure cases:

- No matching city found
- Forecast data is unavailable
- External API returns a non-success HTTP status
- Daily forecast arrays have mismatched counts

For API failures, the client throws an HttpRequestException with the response status code and response body.

## Notes

This is a small interview-style project, not a production weather application.

The aim is to demonstrate simple, practical API integration using modern .NET patterns without unnecessary complexity.

## Possible Improvements

Future improvements could include:

- Unit tests for WeatherDataHandler
- Better structured result/error objects instead of throwing exceptions from API clients
- Logging via ILogger
- Configuration through appsettings.json
- Configurable forecast days
- Support for different time zones
- Minimal API endpoint returning JSON
- GitHub Actions build check
