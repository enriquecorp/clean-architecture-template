#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["mfe-versions.api/mfe-versions.api.csproj", "mfe-versions.api/"]
RUN dotnet restore "mfe-versions.api/mfe-versions.api.csproj"
COPY . .
WORKDIR "/src/mfe-versions.api"
RUN dotnet build "mfe-versions.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mfe-versions.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mfe-versions.api.dll"]