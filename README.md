# What I need to run this project?
  1. .NET 8 SDK Installed
  2. VSCode or Visual Studio
  3. A connection string to MSSQL Server

# How to run this project?
  1. Clone this project into a folder of your prefence
  2. Go to the file "appsettings.json" and change the key-value of the field "DefaultConnection" to a connection string of your MSSQL Server
  3. Use this value to save your time: "Server=YOUR_SQLSERVER_NAME;Database=YOUR_DB_NAME;Trusted_Connection=True;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;"
  4. Go to the console and type "dotnet ef update database" and wait till the migration's done
  5. Again, type 'dotnet watch run' in your console and test the app via Swagger

# What I'll find in this project?
A basic Web API in the version 8 of .NET, which have:
  1. Authentication/Authorization
  2. Repository pattern
  3. Mappers
  4. Entity Framework/Migrations
  5. Many-to-many, One-to-many and one-to-one relationships
  6. Swagger
