# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy all project files to the container
COPY . .

# Ensure the Authentication folder and users.json are copied into the container
COPY ./CargoHubAlt/Authentication/ /app/Authentication/

# Restore dependencies
RUN dotnet restore

# Install the coverlet and reportgenerator tools
RUN dotnet tool install --global coverlet.console
RUN dotnet tool install --global dotnet-reportgenerator-globaltool

# Set the path to the tools
ENV PATH="$PATH:/root/.dotnet/tools"

# Build the test project
RUN dotnet build --no-restore --configuration Debug

# Run the tests
CMD ["dotnet", "test", "/app/IntegrationTests/IntegrationTests.csproj", "--no-build", "--logger:trx", "--results-directory", "/testresults"]
