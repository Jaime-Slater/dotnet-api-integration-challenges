# .NET API Integration Challenges

A collection of small C#/.NET API integration challenges built to demonstrate practical backend development skills.

These projects focus on consuming external REST APIs, mapping JSON responses into C# models, transforming data, and producing useful summaries.

## Purpose

This repository is intended as a portfolio of interview-style coding exercises.

The aim is to show practical skills in:

- REST API consumption
- HttpClient
- JSON deserialisation
- DTO design
- Data mapping and transformation
- LINQ
- Error handling
- Console reporting
- Clean project structure

## Challenges

### JSONPlaceholder Task Summary

Folder:

```
JsonPlaceholderTaskSummary/
```

A console application that retrieves users and todos from JSONPlaceholder, joins the datasets, and calculates task completion statistics per user.

Skills shown:

- Calling multiple API endpoints
- Mapping JSON responses into C# DTOs
- Joining related API data by ID
- Calculating completion percentages
- Producing a readable summary report

### OpenMeteo Weather Summary

Folder:

```
OpenMeteoWeatherSummary/
```

A console application that retrieves location and weather forecast data, maps array-based API responses, and calculates a simple weather risk summary.

Skills shown:

- Calling dependent APIs
- Mapping external DTOs into internal models
- Working with daily forecast arrays
- Calculating average temperature and rainfall summaries
- Applying simple business rules to API data

## Tech Stack

- C#
- .NET 8
- Console applications
- HttpClient
- System.Net.Http.Json

## Repository Structure

```
dotnet-api-integration-challenges/
  JsonPlaceholderTaskSummary/
  OpenMeteoWeatherSummary/
  README.md
```

## Running a Challenge

Open the folder for the challenge you want to run.

Example:

```
cd JsonPlaceholderTaskSummary
dotnet restore
dotnet run
```

Or:

```
cd OpenMeteoWeatherSummary
dotnet restore
dotnet run
```

## Current Status

| Challenge                    | Status   |
| ---------------------------- | -------- |
| JSONPlaceholder Task Summary | Complete |
| OpenMeteo Weather Summary    | Complete |

## Notes

These are deliberately small projects. They are not intended to be full production systems.

The goal is to demonstrate clear, practical API integration skills without unnecessary complexity.

## Possible Future Improvements

Future improvements may include:

- Dependency injection
- IHttpClientFactory
- Unit tests
- Minimal API endpoints
- Better structured error handling
- Logging
- Configuration via appsettings.json
