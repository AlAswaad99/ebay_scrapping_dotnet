
# Project Setup Guide

This guide will walk you through running your application using Docker Compose, which will start up your MSSQL Server, scrapper service, and products service.

### Prerequisites

-   Docker installed on your machine
-   Docker Compose installed on your machine

### Step 1: Clone the Repository

Clone your repository to your local machine:
```
git clone https://github.com/AlAswaad99/ebay_scrapping_dotnet.git
cd ebay_scrapping_dotnet
``` 

### Step 2: Configure Environment Variables

Create a `.env.dev` according to the `.env.example` file in the root of the project directory i.e. where `docker-compose.yml` file exists and provide values for each environment variable. Use the same values that are provided in the example file for `MSSQL_SA_USERNAME` and `ASPNETCORE_ENVIRONMENT`. Use a 256-bit randomly generated string for the value of `JWTSETTINGS__KEY`.

### Step 3: Run The Application

Navigate to the root directory of your project and run the following command:

`docker-compose --env-file .env.dev up -d` 

This command will start up the MSSQL Server, scrapper service, and products service using the configuration defined in the `docker-compose.yml` file.

### Step 4: Access Your Application

Once your services are up and running, you can access the application (prefferably the swagger API dashboard) using the following endpoints:

-   Products Service: `http://localhost:${PRODUCTS_PORT}/swagger`
	Replace the `${PRODUCTS_PORT}` with the one you set in the .env

### Additional Notes

-   The scrapper service and products service containers will wait for the MSSQL Server container to be up and running before starting up. This ensures that the required database is available before attempting to connect.
----------
