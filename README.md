
# Project Setup Guide

This guide provides step-by-step instructions on how to set up Docker containers for the MSSQL server, scrapper service, and products service.

## MSSQL Server

`docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=${DB_PASSWORD}" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`

Replace `${DB_PASSWORD}` with your desired password.

## Scrapper Service

1. Navigate to the location of the Dockerfile for the scrapper service i.e. `/ebay_product_scrapper`.
2. Build the Docker image using the following command:

`docker build -t ebay-scrapper-service:latest .`

3. Once the image is built, run the scrapper service with the provided command:

`docker run -d -e DATABASE_URL="jdbc:sqlserver://host.docker.internal:1433;databaseName=${DB_NAME};trustServerCertificate=true" -e DATABASE_USERNAME="sa" -e DATABASE_PASSWORD=${DB_PASSWORD} -p 8081:8081 ebay-scrapper-service:latest`

Replace `${DB_PASSWORD}` and `${DB_NAME}` with your actual database password and Database Name.

## Products Service

1. Navigate to the location of the Dockerfile for the products service i.e. `/EbayProductsScrappingAssignment/EbayProductsBackend`.
2. Build the Docker image using the following command:

`docker build -t ebay_products_backend .`

3. Once the image is built, run the products service with the provided command:

`docker run -d -p 8080:8080 CONNECTIONSTRINGS__DEFAULTCONNECTION="Server=host.docker.internal,1433;Database=${DB_NAME};User=sa;Password=${DB_PASSWORD};TrustServerCertificate=True;" -e JWTSETTINGS__KEY="${JWT_SECRET_KEY}" -e JWTSETTINGS__ISSUER="${JWT_ISSUER}" -e JWTSETTINGS__AUDIENCE="${JWT_AUDIENCE}" -e SCRAPPERBASEURL="http://localhost:8080/" -e ASPNETCORE_ENVIRONMENT="Development" ebay_products_backend`

Replace `${DB_PASSWORD}` and `${DB_NAME}` with your actual database password and Database Name. Also, generate a random 256 bits string and replace `${JWT_SECRET_KEY}` with it. Then replace `${JWT_ISSUER}` and `${JWT_AUDIENCE}` with your JWT Issuer (use `EbayProductsBackend` for dev environment)

Finally, open up browser and navigate to `http://localhost:8080/swagger/index.html` to open Swagger and access the Web API.
