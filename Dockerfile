# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and project files
COPY Skypoint.API/Skypoint.API.sln ./Skypoint.API.sln
COPY Skypoint.API/Skypoint.API/Skypoint.API.csproj ./Skypoint.API/
COPY Skypoint.API/Skypoint.Application/Skypoint.Application.csproj ./Skypoint.Application/
COPY Skypoint.API/Skypoint.Infrastructure/Skypoint.Infrastructure.csproj ./Skypoint.Infrastructure/
COPY Skypoint.API/Skypoint.Domain/Skypoint.Domain.csproj ./Skypoint.Domain/

# Restore all projects
RUN dotnet restore Skypoint.API.sln

# Copy everything else
COPY Skypoint.API/ ./Skypoint.API/

# Build and publish release
RUN dotnet publish Skypoint.API.sln -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "Skypoint.API.dll"]
