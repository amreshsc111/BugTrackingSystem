FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BugTrackingSystem.API/BugTrackingSystem.API.csproj", "BugTrackingSystem.API/"]
COPY ["BugTrackingSystem.Application/BugTrackingSystem.Application.csproj", "BugTrackingSystem.Application/"]
COPY ["BugTrackingSystem.Domain/BugTrackingSystem.Domain.csproj", "BugTrackingSystem.Domain/"]
COPY ["BugTrackingSystem.Infrastructure/BugTrackingSystem.Infrastructure.csproj", "BugTrackingSystem.Infrastructure/"]
COPY "Directory.Build.props" .
COPY "Directory.Packages.props" .
RUN dotnet restore "./BugTrackingSystem.API/BugTrackingSystem.API.csproj"
COPY . .
WORKDIR "/src/BugTrackingSystem.API"
RUN dotnet build "./BugTrackingSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BugTrackingSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BugTrackingSystem.API.dll"]
