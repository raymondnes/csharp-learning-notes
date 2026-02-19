# Topic 37: Docker Basics for .NET

## Introduction

Docker packages applications into containers - standardized units that include everything needed to run: code, runtime, libraries, and settings. This ensures consistency across development, testing, and production.

## Why Docker?

- **Consistency**: "Works on my machine" → "Works everywhere"
- **Isolation**: Each app runs in its own container
- **Portability**: Run anywhere Docker is installed
- **Scalability**: Easy to replicate containers
- **DevOps**: Simplifies CI/CD pipelines

## Key Concepts

| Term | Description |
|------|-------------|
| **Image** | Blueprint/template for containers |
| **Container** | Running instance of an image |
| **Dockerfile** | Instructions to build an image |
| **Registry** | Storage for images (Docker Hub, ACR) |
| **Volume** | Persistent storage for containers |

## Basic Dockerfile for .NET

```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY ["MyApi/MyApi.csproj", "MyApi/"]
RUN dotnet restore "MyApi/MyApi.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/MyApi"
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "MyApi.dll"]
```

## Multi-Stage Build Explained

```dockerfile
# Stage 1: SDK image for building (large, ~700MB)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Contains compilers, tools for building

# Stage 2: Smaller runtime image (~200MB)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
# Only contains runtime, not SDK
```

## Docker Commands

### Building Images

```bash
# Build image from Dockerfile in current directory
docker build -t myapp:1.0 .

# Build with specific Dockerfile
docker build -f Dockerfile.prod -t myapp:prod .

# Build with build arguments
docker build --build-arg VERSION=1.0 -t myapp .
```

### Running Containers

```bash
# Run container
docker run myapp:1.0

# Run with port mapping
docker run -p 8080:80 myapp:1.0

# Run in background (detached)
docker run -d -p 8080:80 myapp:1.0

# Run with environment variables
docker run -e "ASPNETCORE_ENVIRONMENT=Production" -p 8080:80 myapp:1.0

# Run with name
docker run -d --name my-api -p 8080:80 myapp:1.0
```

### Managing Containers

```bash
# List running containers
docker ps

# List all containers (including stopped)
docker ps -a

# Stop container
docker stop my-api

# Start stopped container
docker start my-api

# Remove container
docker rm my-api

# View logs
docker logs my-api
docker logs -f my-api  # Follow logs

# Execute command in running container
docker exec -it my-api /bin/bash
```

### Managing Images

```bash
# List images
docker images

# Remove image
docker rmi myapp:1.0

# Pull image from registry
docker pull mcr.microsoft.com/dotnet/aspnet:8.0

# Push image to registry
docker push myregistry.azurecr.io/myapp:1.0
```

## .dockerignore

Exclude files from the build context:

```
# .dockerignore
**/.git
**/.vs
**/bin
**/obj
**/node_modules
**/*.md
**/Dockerfile*
**/.dockerignore
*.sln
.gitignore
```

## Environment Variables

```dockerfile
# In Dockerfile
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__Default="Server=db;Database=App"
```

```bash
# At runtime
docker run -e "ASPNETCORE_ENVIRONMENT=Development" \
           -e "ConnectionStrings__Default=Server=localhost" \
           myapp:1.0
```

## Volumes

Persist data outside container lifecycle:

```bash
# Named volume
docker run -v myapp-data:/app/data myapp:1.0

# Bind mount (host directory)
docker run -v /host/path:/container/path myapp:1.0
docker run -v $(pwd)/logs:/app/logs myapp:1.0
```

## Docker Compose

Manage multi-container applications:

```yaml
# docker-compose.yml
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8080:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__Default=Server=db;Database=AppDb;User=sa;Password=YourPassword123!
    depends_on:
      - db
    networks:
      - app-network

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword123!
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql
    networks:
      - app-network

  redis:
    image: redis:alpine
    ports:
      - "6379:6379"
    networks:
      - app-network

networks:
  app-network:
    driver: bridge

volumes:
  sqldata:
```

### Docker Compose Commands

```bash
# Start all services
docker-compose up

# Start in background
docker-compose up -d

# Build and start
docker-compose up --build

# Stop all services
docker-compose down

# Stop and remove volumes
docker-compose down -v

# View logs
docker-compose logs
docker-compose logs api

# Scale service
docker-compose up -d --scale api=3
```

## Health Checks

```dockerfile
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost/health || exit 1
```

```csharp
// In ASP.NET Core
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString)
    .AddRedis(redisConnectionString);

app.MapHealthChecks("/health");
```

## Optimized Dockerfile

```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only csproj files first (better caching)
COPY ["src/MyApi/MyApi.csproj", "src/MyApi/"]
COPY ["src/MyApi.Core/MyApi.Core.csproj", "src/MyApi.Core/"]
RUN dotnet restore "src/MyApi/MyApi.csproj"

# Copy source and build
COPY . .
WORKDIR "/src/src/MyApi"
RUN dotnet publish -c Release -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Security: Run as non-root user
RUN adduser --disabled-password --gecos '' appuser
USER appuser

WORKDIR /app
COPY --from=build /app/publish .

# Health check
HEALTHCHECK --interval=30s --timeout=3s \
  CMD curl -f http://localhost:80/health || exit 1

EXPOSE 80
ENTRYPOINT ["dotnet", "MyApi.dll"]
```

## Development with Docker

```yaml
# docker-compose.override.yml (development)
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile.dev
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - DOTNET_USE_POLLING_FILE_WATCHER=1
    volumes:
      - ./src:/app/src
      - ~/.nuget/packages:/root/.nuget/packages:ro
    ports:
      - "5000:80"
```

```dockerfile
# Dockerfile.dev
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY . .
RUN dotnet restore
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:80"]
```

## Best Practices

1. **Use multi-stage builds** - Smaller final images
2. **Order Dockerfile commands** - Most stable first (better caching)
3. **Use .dockerignore** - Smaller build context
4. **Don't run as root** - Security
5. **Use specific tags** - Not `latest`
6. **Health checks** - Container orchestration
7. **Environment variables** - Configuration

## Summary

| Command | Purpose |
|---------|---------|
| `docker build` | Create image from Dockerfile |
| `docker run` | Start container from image |
| `docker-compose up` | Start multi-container app |
| `-p 8080:80` | Map host:container ports |
| `-v data:/app` | Mount volume |
| `-e VAR=value` | Set environment variable |

Docker is essential for modern deployment and DevOps practices.
