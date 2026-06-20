using System.Net.Http.Json;
using Models;

namespace Services;

public class JsonPlaceholderClient
{
    private readonly HttpClient _httpClient;

    public JsonPlaceholderClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
        };
    }

    public async Task<List<User>> GetUsersAsync()
    {
        //call users
        var response = await _httpClient.GetFromJsonAsync<List<User>>("users");
        if (response?.Count > 0)
        {
            Console.WriteLine($"Users retrieved: {response.Count}");
            return response;
        }

        Console.WriteLine("Failed to get any users");

        return [];
    }

    public async Task<List<Todo>> GetTodosAsync()
    {
        //call todos
        var response = await _httpClient.GetFromJsonAsync<List<Todo>>("todos");
        if (response?.Count > 0)
        {
            Console.WriteLine($"Todos retrieved: {response.Count}");
            return response;
        }

        Console.WriteLine("Failed to get any todos");

        return [];
    }
}