FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# Create non-root user
RUN addgroup --system --gid 1002 netuser && \
    adduser --system --uid 1002 --gid 1002 netuser
WORKDIR /app
RUN chown -R netuser:netuser /app
USER netuser
EXPOSE 9000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["networking_service/networking_service.csproj", "networking_service/"]
RUN dotnet restore "networking_service/networking_service.csproj" \
    --runtime linux-x64 \
    --no-cache

COPY . .
WORKDIR "/src/networking_service"

RUN dotnet build "./networking_service.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build \
    --runtime linux-x64 \
    --self-contained false \
    --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./networking_service.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --runtime linux-x64 \
    --self-contained false \
    --no-restore \
    /p:UseAppHost=false \
    /p:PublishTrimmed=false \
    /p:PublishSingleFile=false

FROM base AS final
WORKDIR /app

COPY --from=publish --chown=netuser:netuser /app/publish .

ENTRYPOINT ["dotnet", "networking_service.dll"]