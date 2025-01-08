# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project files to the container
COPY . . 
# COPY ./CargoHubAlt /app/CargoHubAlt
# COPY ./IntegrationTests /app/IntegrationTests

# Ensure the Authentication folder with users.json is copied as well
COPY ./Authentication /app/IntegrationTests/bin/Debug/net8.0/Authentication/

# Restore dependencies
RUN dotnet restore

# Build the test project
RUN dotnet build --no-restore --configuration Debug

# Create a directory for test results
RUN mkdir /testresults

# Run the tests
CMD ["dotnet", "test", "/app/IntegrationTests/IntegrationTests.csproj", "--no-build", "--logger:trx", "--results-directory", "/testresults"]
