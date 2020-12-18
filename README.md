# AxionBiosystemsChallenge
Creates a command line searchable database of employee data for small co. from a CSV.  The projects uses the Microsoft.NETCore.App 5.0.0 runtime because a .NETCore console application best fits the requirements.

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
Once installation is complete, the following commands can be used to interact with the program.
### Help Menu
```
dotnet run --h
```
or 
```
dotnet run -help
```
### Name Query
```
dotnet run -name '[lastname]'
```
or
```
dotnet run -Name '[lastname]'
```
The last name must be in quotes if it contains a white space. 

Example: ```dotnet run -name Willshee```

### ID Query
```
dotnet run -id [number]
```
or
```
dotnet run -ID [number]
```
Example: ```dotnet run -id 1```

### Name and ID Query
```
dotnet run -name '[lastname]' -id [number]
```
Example: ```dotnet run -name Willshee -id 5```
## Design Considerations

- Used code from https://www.codeproject.com/articles/415732/reading-and-writing-csv-files-in-csharp
to ingest the csv file of employee data and convert to database tables.
- Assuming program should query the db and not modify either the db or the original csv.
- Db queries return first matching entry for both -id and -name args.  If both args are specified, program returns two (possibly identical) db entries if they exist.