FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
# Create non-root user for security
RUN addgroup -g 1001 -S appuser && \
    adduser -u 1001 -S appuser -G appuser
USER appuser
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["web_service/web_service.csproj", "web_service/"]
RUN dotnet restore "web_service/web_service.csproj" \
    --runtime alpine-x64 \
    --no-cache

COPY . .
WORKDIR "/src/web_service"

RUN dotnet build "./web_service.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build \
    --runtime alpine-x64 \
    --self-contained false \
    --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./web_service.csproj" \
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

COPY --from=publish --chown=appuser:appuser /app/publish .

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "web_service.dll"]