FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
# Create non-root user for security
RUN addgroup -g 1001 -S appuser && \
    adduser -u 1001 -S appuser -G appuser
USER appuser
WORKDIR /app
RUN chown -R appuser:appuser /app
EXPOSE 9001

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["web_service/web_service.csproj", "web_service/"]
RUN dotnet restore "web_service/web_service.csproj" \
    --runtime linux-musl-x64 \
    --no-cache

COPY . .
WORKDIR "/src/web_service"

RUN dotnet build "./web_service.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build \
    --runtime linux-musl-x64 \
    --self-contained false \
    --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./web_service.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --runtime linux-musl-x64 \
    --self-contained false \
    --no-restore \
    /p:UseAppHost=false \
    /p:PublishTrimmed=false \
    /p:PublishSingleFile=false

FROM base AS final
WORKDIR /app

COPY --from=publish --chown=appuser:appuser /app/publish .

ENTRYPOINT ["dotnet", "web_service.dll"]