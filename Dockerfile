FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# IMPORTANT FIX
ENV DOTNET_USE_POLLING_FILE_WATCHER=false
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app .

ENTRYPOINT ["dotnet", "RunEmpire.Api.dll"]

