FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY ["src/MovieService/MovieService.csproj", "src/MovieService/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
RUN dotnet restore "src/MovieService/MovieService.csproj"

COPY . .

WORKDIR "/src/MovieService"
RUN dotnet build "MovieService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieService.dll"]