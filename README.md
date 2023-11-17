# TaxCalculator Solution

## Overview
The `TaxCalculator` is a code-first application designed to calculate taxes based on postal codes and income. It's structured using the Repository pattern to abstract the data layer and promote a testable, maintainable architecture.

## Running the Application
Run `TaxCalculator.Api` and `TaxCalculator.Web` simultaneously to start both backend and frontend services. The database will be created and seeded on the API's first run.

## Structure
- `TaxCalculator.Api`: The entry point for the application, hosting the RESTful API.
- `TaxCalculator.Entities`: Contains the entity models representing the database.
- `TaxCalculator.Repository`: Implements the Repository pattern for data access.
- `TaxCalculator.Service`: Contains business logic and service layer abstractions.
- `TaxCalculator.Test`: Includes unit tests to ensure code reliability.
- `TaxCalculator.Web`: Provides a user interface to interact with the system.

## Setup
Prerequisites include .NET Core and SQL Server LocalDB.

## Database Configuration
The application is configured to connect to a SQL Server LocalDB instance. The connection string is specified in the `appsettings.json` file within the `TaxCalculator.Api` project, allowing for easy management and updates:

```json
"ConnectionStrings": {
  "TaxCalculatorApiContext": "Server=(localdb)\\MSSQLLocalDB;Database=TaxCalculatorV1;Trusted_Connection=True;MultipleActiveResultSets=true"
}
# TaxCalculator
