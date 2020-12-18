# AxionBiosystemsChallenge
Creates a command line searchable database of employee data for small co. from a CSV.  The projects uses the Microsoft.NETCore.App 5.0.0 runtime. The alternatives were an older .netcore, asp, xamarin (find a few others and compare pros and cons).  Asp is used for GUI based web applications, so I think the latest .NETCore and a console application fits the requirements better.

## Installation

### Create New .NETCore Console Application
```
dotnet new console
dotnet build
dotnet run
```

### Install Entity Framework (EF) Core
https://www.entityframeworktutorial.net/efcore/install-entity-framework-core.aspx
https://docs.microsoft.com/en-us/ef/core/cli/dotnet

```
dotnet tool install --global dotnet-ef
```

### Add the Sqlite Database Package
https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

### Create the Database
```
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Usage

## Design Considerations

Used code from https://www.codeproject.com/articles/415732/reading-and-writing-csv-files-in-csharp
to ingest the csv file of employee data and convert to database tables.