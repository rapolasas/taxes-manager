# taxes-manager

Before running integration tests you must have database_container running. To do so follow these steps:
1. Set ```docker-compose``` as start-up project
2. Run from Visual Studio
3. Stop project Visual Studio
4. Run integration tests

Before running integrations tests, change ```Database``` connection string to ```ApiTestsDatabase``` in ```TaxesManager.Infrastructure/ConfigureServices.cs```. Because of lack of time it was not fully implemented to work without needing to make any changes
