# HadiShop

## Project Information

This project uses:

- **.NET 8**
- **SQL Server 2022**

## Database Setup

1. **Create the Database**

   Before running the application, you need to create a database named `hadiShop`. You can do this using SQL Server Management Studio (SSMS) or SQL Server Command Line tools.

   **Using SQL Server Management Studio (SSMS):**
   - Open SQL Server Management Studio and connect to your SQL Server instance.
   - Right-click on the `Databases` node and select `New Database...`.
   - Enter `hadiShop` as the database name and click `OK`.

   **Using SQL Server Command Line:**
   - Open Command Prompt or PowerShell.
   - Use the following commands to create the database:

     ```sql
     sqlcmd -S <ServerName> -U <Username> -P <Password>
     CREATE DATABASE hadishop;
     GO
     ```

     Replace `<ServerName>`, `<Username>`, and `<Password>` with your SQL Server instance details.

## Running the Project

You can run the project using either Visual Studio or Command Prompt (CMD). Follow the instructions for your preferred method.

### Using Visual Studio

1. **Open the Project**

   - Launch Visual Studio.
   - Click on `File` > `Open` > `Project/Solution...`.
   - Navigate to the project directory and open the `.sln` file.

2. **Configure the Connection String**

   - Open `appsettings.json` in the Solution Explorer.
   - Update the connection string with your SQL Server details:

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=<ServerName>;Database=hadiShop;User Id=<Username>;Password=<Password>;"
     }
     ```

     Replace `<ServerName>`, `<Username>`, and `<Password>` with your SQL Server instance details.

3. **Build and Run**

   - Click on `Build` > `Build Solution` to build the project.
   - Click on `Debug` > `Start Debugging` to run the project. The application will start in your default web browser.

### Using Command Prompt (CMD)

1. **Navigate to Project Directory**

   Open Command Prompt and navigate to the project directory where the `.csproj` file is located.

   ```cmd
   cd path\to\your\project
   ```

2. **Restore Dependencies**

   Run the following command to restore the project dependencies:

   ```cmd
   dotnet restore
   ```

3. **Apply Migrations**

   Apply database migrations to create the schema in the `hadiShop` database:

   ```cmd
   dotnet ef database update
   ```

4. **Run the Project**

   Run the application using the following command:

   ```cmd
   dotnet run
   ```

   The application will start, and you can access it in your web browser.
