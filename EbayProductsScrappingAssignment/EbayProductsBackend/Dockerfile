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

# Install wget
# RUN apt-get update && \
#     apt-get install -y netcat-openbsd wget

# Set the working directory in the container
WORKDIR /app

# Copy the published output from the previous stage to the container
COPY --from=build /app/out .

EXPOSE 8080

# Add wait-for-it for docker-compose
# COPY wait-for-it.sh wait-for-it.sh
# RUN chmod +x wait-for-it.sh
# RUN sed -i 's/\r$//' wait-for-it.sh

# # Wait for 30 seconds before running the entrypoint command
# RUN sleep 30

# Run the application
ENTRYPOINT ["dotnet", "EbayProductsBackend.dll"]
