# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /CargohubAlt

# Copy the project files to the container
COPY . .
COPY ./IntegrationTests .

# Restore dependencies
RUN dotnet restore

# Build the test project
RUN dotnet build --no-restore --configuration Debug

# Run the tests
CMD ["dotnet", "test", "IntegrationTests/IntegrationTests.csproj", "--no-build", "--logger:trx", "--results-directory", "/testresults"]
