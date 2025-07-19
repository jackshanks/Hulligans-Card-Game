FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
# Create non-root user for security
RUN addgroup -g 1002 -S netuser && \
    adduser -u 1002 -S netuser -G netuser
USER netuser
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["networking_service/networking_service.csproj", "networking_service/"]
RUN dotnet restore "networking_service/networking_service.csproj" \
    --runtime alpine-x64 \
    --no-cache

COPY . .
WORKDIR "/src/networking_service"

RUN dotnet build "./networking_service.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build \
    --runtime alpine-x64 \
    --self-contained false \
    --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./networking_service.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --runtime alpine-x64 \
    --self-contained false \
    --no-restore \
    /p:UseAppHost=false \
    /p:PublishTrimmed=false \
    /p:PublishSingleFile=false

FROM base AS final
WORKDIR /app

COPY --from=publish --chown=netuser:netuser /app/publish .

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "networking_service.dll"]