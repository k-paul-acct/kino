FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY Kino.Api/Kino.Api.csproj Kino.Api/
RUN dotnet restore "Kino.Api/Kino.Api.csproj"
COPY . .
WORKDIR /src/Kino.Api
RUN dotnet build "Kino.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kino.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kino.Api.dll"]