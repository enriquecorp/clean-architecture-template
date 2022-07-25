#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ["src/Apps/mfe-versions.api/mfe-versions.api.csproj", "src/Apps/mfe-versions.api/"]
COPY ["src/Versioning.Infrastructure/Versioning.Infrastructure.csproj", "src/Versioning.Infrastructure/"]
COPY ["src/Shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/Shared/Shared.Infrastructure/"]
COPY ["src/Shared/Shared.Infrastructure.Bus/Shared.Infrastructure.Bus.csproj", "src/Shared/Shared.Infrastructure.Bus/"]
COPY ["src/Shared/Shared.Domain/Shared.Domain.csproj", "src/Shared/Shared.Domain/"]
COPY ["src/Shared/Shared.Infrastructure.Web/Shared.Infrastructure.Web.csproj", "src/Shared/Shared.Infrastructure.Web/"]
COPY ["src/Versioning.Domain/Versioning.Domain.csproj", "src/Versioning.Domain/"]
COPY ["src/Versioning.Service/Versioning.Service.csproj", "src/Versioning.Service/"]
RUN dotnet restore "src/Apps/mfe-versions.api/mfe-versions.api.csproj"
COPY . .
WORKDIR "/app/src/Apps/mfe-versions.api"
RUN dotnet build "mfe-versions.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mfe-versions.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mfe-versions.api.dll"]