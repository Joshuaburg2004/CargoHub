# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy all project files from the IntegrationTests directory to the container
COPY ./IntegrationTests ./

# Copy the Authentication folder from CargoHubAlt to the container
COPY ./CargoHubAlt/Authentication /app/Authentication/

# Restore dependencies
RUN dotnet restore

# Build the test project
RUN dotnet build --no-restore --configuration Debug

# Run the tests
CMD ["dotnet", "test", "/app/IntegrationTests/IntegrationTests.csproj", "--no-build", "--logger:trx", "--results-directory", "/testresults"]
