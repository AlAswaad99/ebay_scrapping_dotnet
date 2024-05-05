# Use the official .NET Core SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:latest AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project files to the container
COPY . .
RUN dotnet restore


COPY . ./
RUN dotnet publish -c Release -o out

# Create a new stage for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:latest AS runtime

# Set the working directory in the container
WORKDIR /app

# Copy the published output from the previous stage to the container
COPY --from=build /app/out .

# RUN dotnet tool install --global dotnet-ef

# RUN dotnet ef database update --context DataContext --startup-project EbayProductsBackend -c EbayProductsBackend -p EbayProductsBackend

# Expose port 8080 for HTTP traffic
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "EbayProductsBackend.dll"]
