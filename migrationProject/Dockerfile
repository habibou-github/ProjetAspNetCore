# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .X
ENTRYPOINT ["dotnet", "migrationProject.dll"]

#docker network create asp_net
# docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<1234@amine>" --restart always -p 1433:1433 --net asp_net --name sql1 -d mcr.microsoft.com/mssql/server:2019-latest
# docker run --name aspcore --net asp_net --restart always -p 7777:80 -d aspcore