# Define the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the source code into the container
COPY *.csproj ./

# Restore the NuGet packages
RUN dotnet restore

COPY .  ./

# Build the application
RUN dotnet publish -c Release -o out

# Define the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the built application from the build image
COPY --from=build /app/out .



# Start the application
ENTRYPOINT ["dotnet", "PlatformService.dll"]
