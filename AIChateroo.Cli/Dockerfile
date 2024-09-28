FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AIChateroo.Cli/AIChateroo.Cli.csproj", "AIChateroo.Cli/"]
RUN dotnet restore "AIChateroo.Cli/AIChateroo.Cli.csproj"
COPY . .
WORKDIR "/src/AIChateroo.Cli"
RUN dotnet build "AIChateroo.Cli.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AIChateroo.Cli.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AIChateroo.Cli.dll"]
