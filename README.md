# Library Management System (ASP.NET Core Web API)

##  Requirements

- [.NET 9 SDK or later](https://dotnet.microsoft.com/download)
- [SQL Server Express (Preffered) / SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Code Editor (like VSCode)
- Have Access to a terminal
- EF Core Tools:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

  ## Setup
  - Initialize project:
    ```bash
    dotnet restore
    ```
  - If necessary, open appsettings.json and modify Server field of conection string with the name of your server instance
  - Create the database:
    ```bash
    dotnet ef database update
    ```
  - Run the application:
    ```bash
    dotnet run
    ```
  - Test the API via Swagger UI at: http://localhost:5181/swagger

## Extra Functionality
  - Retrieve the top N most borrowed books in the library. <br> Results are given in descending order of total borrows. <br> N is a parameter picked by the user.
