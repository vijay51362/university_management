# University ManagementSystem-
University Management System with .NET Core MVC and Neon PostgreSQL
1. Set Up Your Environment
First, ensure you have these installed:

.NET SDK (version matching your project)

PostgreSQL (or your Neon PostgreSQL connection ready)

2. Configure the Database Connection
Edit appsettings.json with your Neon PostgreSQL credentials:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your-neon-host;Database=your-db-name;Username=your-user;Password=your-password;SSL Mode=Require"
  }
}
3. Apply Database Migrations
Run these commands in your project directory:

bash
# Apply pending migrations
dotnet ef database update

# Or if you need to create new migrations
dotnet ef migrations add InitialSetup
dotnet ef database update
4. Run the Application
Start the application with:

bash
dotnet run
5. Access the Application
The MVC web interface will be available at:
https://localhost:5001 or http://localhost:5000

If you added Swagger, access the API docs at:
https://localhost:5001/swagger
