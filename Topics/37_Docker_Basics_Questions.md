# Topic 37: Docker Basics - Assessment Questions

## Level 1 (Trivial)

**Question:** What is the difference between a Docker image and a container?

**Expected Solution:**
- **Image**: A read-only template/blueprint containing application code, runtime, libraries, and dependencies. It's like a class definition.
- **Container**: A running instance of an image. It's like an object instantiated from a class. You can have multiple containers from the same image.

---

## Level 2 (Trivial)

**Question:** Write the Docker command to run a container with port mapping and environment variable.

**Expected Solution:**
```bash
docker run -d -p 8080:80 -e "ASPNETCORE_ENVIRONMENT=Production" myapp:1.0
```

Explanation:
- `-d`: Run in detached mode (background)
- `-p 8080:80`: Map host port 8080 to container port 80
- `-e`: Set environment variable
- `myapp:1.0`: Image name and tag

---

## Level 3 (Easy)

**Question:** Write a basic Dockerfile for an ASP.NET Core application.

**Expected Solution:**
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "MyApi.dll"]
```

---

## Level 4 (Easy)

**Question:** Convert this basic Dockerfile to a multi-stage build.

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/MyApi.dll"]
```

**Expected Solution:**
```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "MyApi.dll"]
```

---

## Level 5 (Moderate)

**Question:** Create a docker-compose.yml for an API with SQL Server.

**Expected Solution:**
```yaml
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
      - ConnectionStrings__Default=Server=db;Database=AppDb;User=sa;Password=SecurePass123!;TrustServerCertificate=true
    depends_on:
      - db
    networks:
      - app-network

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=SecurePass123!
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql
    networks:
      - app-network

networks:
  app-network:
    driver: bridge

volumes:
  sqldata:
```

---

## Level 6 (Moderate)

**Question:** Optimize this Dockerfile for better layer caching.

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MyApi.dll"]
```

**Expected Solution:**
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only project files first (better caching)
COPY ["MyApi/MyApi.csproj", "MyApi/"]
COPY ["MyApi.Core/MyApi.Core.csproj", "MyApi.Core/"]

# Restore as separate layer
RUN dotnet restore "MyApi/MyApi.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR "/src/MyApi"
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "MyApi.dll"]
```

---

## Level 7 (Challenging)

**Question:** Create a complete docker-compose setup with API, database, Redis cache, and health checks.

**Expected Solution:**
```yaml
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8080:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__Default=Server=db;Database=AppDb;User=sa;Password=SecurePass123!;TrustServerCertificate=true
      - Redis__ConnectionString=redis:6379
    depends_on:
      db:
        condition: service_healthy
      redis:
        condition: service_started
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost:80/health"]
      interval: 30s
      timeout: 10s
      retries: 3
      start_period: 40s
    restart: unless-stopped
    networks:
      - app-network

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=SecurePass123!
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql
    healthcheck:
      test: /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "SecurePass123!" -Q "SELECT 1"
      interval: 10s
      timeout: 5s
      retries: 5
      start_period: 30s
    restart: unless-stopped
    networks:
      - app-network

  redis:
    image: redis:7-alpine
    ports:
      - "6379:6379"
    volumes:
      - redisdata:/data
    command: redis-server --appendonly yes
    healthcheck:
      test: ["CMD", "redis-cli", "ping"]
      interval: 10s
      timeout: 5s
      retries: 3
    restart: unless-stopped
    networks:
      - app-network

networks:
  app-network:
    driver: bridge

volumes:
  sqldata:
  redisdata:
```

---

## Level 8 (Challenging)

**Question:** Create a production-ready Dockerfile with security best practices.

**Expected Solution:**
```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

# Install security updates
RUN apk update && apk upgrade

WORKDIR /src

# Copy project files
COPY ["src/MyApi/MyApi.csproj", "src/MyApi/"]
COPY ["src/MyApi.Core/MyApi.Core.csproj", "src/MyApi.Core/"]
COPY ["src/MyApi.Infrastructure/MyApi.Infrastructure.csproj", "src/MyApi.Infrastructure/"]

# Restore
RUN dotnet restore "src/MyApi/MyApi.csproj"

# Copy source
COPY . .

# Build
WORKDIR "/src/src/MyApi"
RUN dotnet publish -c Release -o /app/publish \
    --no-restore \
    /p:UseAppHost=false \
    /p:PublishTrimmed=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

# Install security updates
RUN apk update && apk upgrade && rm -rf /var/cache/apk/*

# Create non-root user
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

# Set up app directory
WORKDIR /app
RUN chown -R appuser:appgroup /app

# Copy published files
COPY --from=build --chown=appuser:appgroup /app/publish .

# Switch to non-root user
USER appuser

# Security headers
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:8080/health || exit 1

EXPOSE 8080

ENTRYPOINT ["dotnet", "MyApi.dll"]
```

---

## Level 9 (Expert)

**Question:** Create a complete containerized development and production setup including:
- Multi-stage Dockerfile optimized for both dev and prod
- docker-compose for development with hot reload
- docker-compose for production with all services
- Proper networking, volumes, and secrets management

**Expected Solution:**
```dockerfile
# Dockerfile
# Development target
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development
WORKDIR /src
COPY . .
RUN dotnet restore
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
EXPOSE 80
ENTRYPOINT ["dotnet", "watch", "run", "--project", "src/MyApi", "--urls", "http://0.0.0.0:80"]

# Build target
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["src/MyApi/MyApi.csproj", "src/MyApi/"]
COPY ["src/MyApi.Core/MyApi.Core.csproj", "src/MyApi.Core/"]
RUN dotnet restore "src/MyApi/MyApi.csproj"
COPY . .
RUN dotnet publish "src/MyApi/MyApi.csproj" -c Release -o /app/publish --no-restore

# Production target
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS production
RUN apk update && apk upgrade && rm -rf /var/cache/apk/*
RUN addgroup -S app && adduser -S app -G app
WORKDIR /app
COPY --from=build --chown=app:app /app/publish .
USER app
ENV ASPNETCORE_URLS=http://+:8080
HEALTHCHECK --interval=30s --timeout=3s CMD wget -q --spider http://localhost:8080/health || exit 1
EXPOSE 8080
ENTRYPOINT ["dotnet", "MyApi.dll"]
```

```yaml
# docker-compose.yml (base)
version: '3.8'

services:
  api:
    networks:
      - app-network

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    networks:
      - app-network
    volumes:
      - sqldata:/var/opt/mssql

  redis:
    image: redis:7-alpine
    networks:
      - app-network
    volumes:
      - redisdata:/data

networks:
  app-network:

volumes:
  sqldata:
  redisdata:
```

```yaml
# docker-compose.dev.yml
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
      target: development
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__Default=Server=db;Database=AppDb;User=sa;Password=DevPass123!;TrustServerCertificate=true
      - Redis__ConnectionString=redis:6379
    volumes:
      - ./src:/src/src
      - ~/.nuget/packages:/root/.nuget/packages:ro
    depends_on:
      - db
      - redis

  db:
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=DevPass123!
    ports:
      - "1433:1433"

  redis:
    ports:
      - "6379:6379"
```

```yaml
# docker-compose.prod.yml
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
      target: production
    deploy:
      replicas: 3
      resources:
        limits:
          cpus: '0.5'
          memory: 512M
        reservations:
          cpus: '0.25'
          memory: 256M
      restart_policy:
        condition: on-failure
        delay: 5s
        max_attempts: 3
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
    secrets:
      - db_password
      - redis_password
    healthcheck:
      test: ["CMD", "wget", "-q", "--spider", "http://localhost:8080/health"]
      interval: 30s
      timeout: 10s
      retries: 3

  db:
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD_FILE=/run/secrets/db_password
    secrets:
      - db_password
    deploy:
      placement:
        constraints:
          - node.role == manager

  redis:
    command: redis-server --requirepass_file /run/secrets/redis_password --appendonly yes
    secrets:
      - redis_password

secrets:
  db_password:
    external: true
  redis_password:
    external: true
```

```bash
# Usage commands
# Development
docker-compose -f docker-compose.yml -f docker-compose.dev.yml up

# Production
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d

# With build
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d --build
```

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | Basic concepts, simple commands |
| 3-4 | Basic Dockerfile, multi-stage builds |
| 5-6 | Docker Compose, layer optimization |
| 7-8 | Health checks, security practices |
| 9 | Complete dev/prod setup |
