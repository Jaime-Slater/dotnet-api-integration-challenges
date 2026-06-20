# JSONPlaceholder Task Completion Challenge

A small .NET console application built as an interview-style API coding challenge.

The application consumes a public REST API, maps the response data into C# models, joins related datasets, calculates task completion statistics, and prints a clean report to the console.

## Purpose

This project demonstrates practical API integration skills in C#/.NET, including:

- Calling external REST APIs with `HttpClient`
- Deserialising JSON responses into DTOs
- Joining related API datasets
- Calculating summary statistics
- Handling simple edge cases
- Producing readable console output
- Keeping the solution simple and interview-appropriate

The project uses [JSONPlaceholder](https://jsonplaceholder.typicode.com/), a free fake REST API for testing and prototyping.

## What the Application Does

The app retrieves:

- Users from `/users`
- Todos from `/todos`

It then joins todos to users by `UserId` and calculates a task completion summary for each user.

For each user, the report includes:

- Name
- Email
- Company name
- Total tasks
- Completed tasks
- Incomplete tasks
- Completion percentage
- Status

The user status is calculated as:

| Completion Percentage | Status      |
| --------------------- | ----------- |
| 80% or higher         | Excellent   |
| 60% or higher         | Good        |
| Less than 60%         | Needs Focus |

The app also prints an overall summary showing:

- Total users
- Total tasks
- Total completed tasks
- Total incomplete tasks
- Average completion percentage
- Best performing user
- Worst performing user

## Tech Stack

- C#
- .NET 8
- Console application
- `HttpClient`
- `System.Net.Http.Json`

## Project Structure

```text
JsonPlaceholderChallenge/
  Models/
    Company.cs
    Todo.cs
    User.cs
    UserTaskSummary.cs

  Services/
    JsonPlaceholderClient.cs

  ConsolePrinter.cs
  Program.cs
  JsonPlaceholderChallenge.csproj
```

## API Endpoints Used

Base URL:

```text
https://jsonplaceholder.typicode.com/
```

Endpoints:

```http
GET /users
GET /todos
```

No authentication or special headers are required.

## How to Run

Clone the repository:

```bash
git clone https://github.com/YOUR-USERNAME/YOUR-REPO-NAME.git
cd YOUR-REPO-NAME
```

Restore and run:

```bash
dotnet restore
dotnet run
```

## Example Output

```text
Task Completion Report

----------
Name: Leanne Graham
Email: Sincere@april.biz
Company name: Romaguera-Crona
Total tasks: 20
Completed tasks: 11
Incomplete tasks: 9
Completion percentage: 55.0%
Status: Needs Focus

----------
Overall Summary

Total Users: 10
Total Tasks: 200
Total Completed: 90
Total Incomplete: 110
Average Completion: 45.0%
Best User: Chelsey Dietrich - 60.0%
Worst User: Patricia Lebsack - 30.0%
```

Actual values may vary if the API data changes.

## Key Implementation Points

The app deliberately keeps the architecture simple because this was built as a short interview-style challenge.

The main areas demonstrated are:

- Keeping DTOs focused on only the fields required
- Separating API access into a small client class
- Avoiding integer division when calculating percentages
- Guarding against empty API responses
- Ordering results by completion percentage
- Formatting output for readability

## Possible Improvements

Given more time, this project could be extended with:

- Dependency injection
- `IHttpClientFactory`
- Unit tests for the summary calculation logic
- A minimal API endpoint returning JSON
- Command-line filtering by status
- Better structured error handling
- Logging

## Notes

This is not intended to be a production application. It is a compact API integration exercise designed to show clean, practical C# coding and basic data transformation skills.
