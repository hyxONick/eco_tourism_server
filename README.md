# Eco-Tourism Microservices

This project consists of multiple microservices for an eco-tourism application, deployed using Docker and Docker Compose.

## Microservices List

- `eco_tourism_gateway` - API Gateway
- `eco_tourism_user` - User Service
- `eco_tourism_tourist` - Tourist Service
- `eco_tourism_accommodation` - Accommodation Service
- `eco_tourism_weather` - Weather Service
- `eco_tourism_outdoor` - Outdoor Activities Service

## Development:

When you completed the DB Model, you can pull migrations to MYSQL
```bash

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add InitialCreate
dotnet ef database update
```

```startup DEV:
```Linux:
chmod +x start_watch_apps_linux.sh
./start_watch_apps_linux.sh
```
```Win:
start_watch_apps_win.bat
```
dotnet watch/run //hot update
```


## System Requirements

- Docker
- Docker Compose

## Setup and Running

### 1. Clone the Repository

First, clone the repository containing the Docker Compose file and microservice projects:

```bash
git clone https://github.com/hyxONick/eco_tourism_server.git
cd eco_tourism_server
```

### 2. Create Docker Network

Create a Docker network for inter-service communication:

```bash
docker network create eco_tourism_network
```

### 3. Build and Start Services

Use Docker Compose to build and start all the microservices:

```bash
docker-compose up --build
```

This command will:
- Build Docker images for each microservice.
- Start all the services defined in the `docker-compose.yml` file.

### 4. Access Services

- The `eco_tourism_gateway` service will be available at `http://localhost:8080`
- The `eco_tourism_user` service will be available at `http://localhost:8081`
- The `eco_tourism_tourist` service will be available at `http://localhost:8082`
- The `eco_tourism_accommodation` service will be available at `http://localhost:8083`
- The `eco_tourism_weather` service will be available at `http://localhost:8084`
- The `eco_tourism_outdoor` service will be available at `http://localhost:8085`

### 5. Stop Services

To stop and remove all running containers, use:

```bash
docker-compose down
```

### 6. Clean Up Unused Docker Images and Networks

To remove unused Docker images and networks, you can use:

```bash
docker system prune -a
```

**Note**: This will remove all unused containers, networks, and images. Ensure you do not need these resources before running this command.

## Notes

- Ensure that the Dockerfile files and source code for all microservices are located in their respective `context` paths.
- If you modify service configurations or Dockerfiles, rebuild the images to apply changes.
