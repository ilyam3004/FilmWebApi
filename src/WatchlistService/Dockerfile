FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY ["src/WatchlistService/WatchlistService.csproj", "src/WatchlistService/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
RUN dotnet restore "src/WatchlistService/WatchlistService.csproj"

COPY . .

WORKDIR "/src/WatchlistService"
RUN dotnet build "WatchlistService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WatchlistService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WatchlistService.dll"]