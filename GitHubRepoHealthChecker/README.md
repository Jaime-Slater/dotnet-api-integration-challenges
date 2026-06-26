# GitHub Repo Health Checker

A small .NET console application that checks the basic health of a public GitHub repository using the GitHub REST API.

The app asks for a GitHub repository owner and repository name, then returns a simple summary including stars, forks, open issues, last updated date, and a calculated health rating.

## Project Goal

The aim of this challenge is to practise:

- Calling a public API with `HttpClient`
- Sending required request headers
- Deserializing JSON into C# POCO classes
- Handling invalid input and API errors
- Working with dates and simple business rules
- Producing clean console output

## API Used

This project uses the GitHub repository API endpoint:

```text
https://api.github.com/repos/{owner}/{repo}
```

Example:

```text
https://api.github.com/repos/dotnet/runtime
```

GitHub requires a `User-Agent` header for API requests.

## Example Usage

```text
GitHub owner:
dotnet

Repository name:
runtime
```

Example output:

```text
Repository: dotnet/runtime
Description: .NET is a cross-platform runtime for cloud, mobile, desktop, and IoT apps.
Language: C#
Stars: 17,000
Forks: 4,300
Open Issues: 5,200
Last Updated: 2026-06-20

Health Rating: Active
```

## Health Rating Rules

The repository health rating is calculated using the following rules:

| Condition                        | Rating     |
| -------------------------------- | ---------- |
| Repository is archived           | Archived   |
| Open issues are over 1,000       | Busy       |
| Updated within the last 30 days  | Active     |
| Updated within the last 180 days | Maintained |
| Anything else                    | Stale      |

Rules are checked in order.

## Required Features

The application should:

- Ask the user for a GitHub owner
- Ask the user for a repository name
- Use `HttpClient` to call the GitHub API
- Include a valid `User-Agent` request header
- Deserialize the JSON response into a POCO
- Display repository details in the console
- Format large numbers with thousands separators
- Convert the `updated_at` API value into a readable date
- Handle invalid owner or repository names
- Handle API errors without crashing

## Suggested Project Structure

```text
GitHubRepoHealthChecker/
  Models/
    GitHubRepositoryResponse.cs
  Program.cs
  GitHubRepoHealthChecker.csproj
  README.md
```

## Model Example

```csharp
using System.Text.Json.Serialization;

public sealed class GitHubRepositoryResponse
{
    [JsonPropertyName("full_name")]
    public string FullName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("stargazers_count")]
    public int StargazersCount { get; set; }

    [JsonPropertyName("forks_count")]
    public int ForksCount { get; set; }

    [JsonPropertyName("open_issues_count")]
    public int OpenIssuesCount { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("archived")]
    public bool Archived { get; set; }
}
```

## Running the Project

From the project directory, run:

```bash
dotnet run
```

Then follow the prompts in the console.

## Example Repositories to Test

```text
dotnet/runtime
dotnet/aspnetcore
microsoft/vscode
facebook/react
torvalds/linux
```

## Stretch Goals

After completing the core version, possible improvements include:

- Keep asking for repositories until the user types `exit`
- Support full GitHub URLs, such as:

```text
https://github.com/dotnet/runtime
```

- Show the number of days since the repository was last updated
- Add colour-coded console output
- Move API logic into a separate service class
- Add unit tests for the health rating rules

## Notes

This is intentionally a small console app. The first version should favour simple, readable code over unnecessary architecture.

A sensible first implementation would use:

- One `Program.cs`
- One model class
- A small helper method for calculating the health rating
- Basic error handling around the HTTP request and JSON deserialization

## Skills Practised

- .NET console app development
- HTTP API integration
- JSON mapping with `System.Text.Json`
- Date handling with `DateTimeOffset`
- Defensive input handling
- Simple business rule implementation
