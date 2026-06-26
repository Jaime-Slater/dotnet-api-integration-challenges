using System.Net.Http.Json;
using System.Text.Json;
using GitHubRepoHealthChecker.Models;

Console.WriteLine("GitHub Repo Health Checker!");

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.github.com/"),
};

//Github requires User Agent header.
httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubRepoHealthChecker/1.0");

while (true)
{
    Console.WriteLine("Enter a GitHub repo owner or type Exit to close the app.");
    string? owner = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(owner))
    {
        Console.WriteLine("No owner entered.");
        continue;
    }
    owner = owner.Trim();

    if (string.Equals(owner, "exit", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Exiting app");
        return;
    }

    bool hasEnteredRepoName = false;
    string? repo = null;

    while (!hasEnteredRepoName)
    {
        Console.WriteLine("Enter a GitHub repo name or type Exit to close the app.");
        repo = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(repo))
        {
            Console.WriteLine("No repo entered.");
            continue;
        }

        repo = repo.Trim();
        hasEnteredRepoName = true;
    }

    if (string.Equals(repo, "exit", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Exiting app");
        return;
    }


    Console.WriteLine($"Looking up GitHub repository: {owner}/{repo}");

    try
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"repos/{owner}/{repo}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"GitHub returned {(int)response.StatusCode} {response.ReasonPhrase}.");
            continue;
        }

        var gitHubRepo = await response.Content.ReadFromJsonAsync<GitHubRepositoryResponse>();

        if (gitHubRepo is null)
        {
            Console.WriteLine($"Failed format data for: {repo} created by: {owner}. Error: GitHubRepositoryResponse is null");
            return;
        }

        var healthRating = GetHealthRating(gitHubRepo);

        Console.WriteLine($"Repository: {gitHubRepo.FullName}");
        Console.WriteLine($"Description: {gitHubRepo.Description}");
        Console.WriteLine($"Language: {gitHubRepo.Language}");
        Console.WriteLine($"Stars: {gitHubRepo.StargazersCount:n0}");
        Console.WriteLine($"Forks: {gitHubRepo.ForksCount:n0}");
        Console.WriteLine($"Open Issues: {gitHubRepo.OpenIssuesCount:n0}");
        Console.WriteLine($"Last Updated: {gitHubRepo.UpdatedAt:yyyy-MM-dd}");
        Console.WriteLine($"");
        Console.WriteLine($"Health Rating: {healthRating}");
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"Request failed: {ex.Message}");
    }
    catch (NotSupportedException ex)
    {
        Console.WriteLine($"Unsupported response format: {ex.Message}");
    }
    catch (JsonException ex)
    {
        Console.WriteLine($"Failed to parse GitHub response: {ex.Message}");
    }

}

string GetHealthRating(GitHubRepositoryResponse gitHubRepo)
{
    if (gitHubRepo.Archived)
        return "Archived";

    if (gitHubRepo.OpenIssuesCount > 1000)
        return "Busy";

    var now = DateTimeOffset.UtcNow;

    if (gitHubRepo.UpdatedAt > now.AddDays(-30))
        return "Active";

    if (gitHubRepo.UpdatedAt > now.AddDays(-180))
        return "Maintained";

    return "Stale";
}