using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Handlers;
using Handlers.Interfaces;
using Services.Interfaces;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHttpClient<IGeocodingClient, GeocodingClient>(client =>
        {
            client.BaseAddress = new Uri("https://geocoding-api.open-meteo.com/v1/");
        });

        services.AddHttpClient<IOpenMeteoClient, OpenMeteoClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.open-meteo.com/v1/");
        });

        services.AddTransient<IWeatherDataHandler, WeatherDataHandler>();
        services.AddTransient<App>();
    })
    .Build();

await host.Services.GetRequiredService<App>().RunAsync(args);
