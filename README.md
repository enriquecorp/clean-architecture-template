# Versioning Service

## How to build the project

```bash
dotnet build cxs-versioning-service.sln
```

## How to run the project

```bash
cd .docker
docker-compose -f docker-compose.localstack.yaml up
dotnet run --project app/mfe-versions.api/mfe-versions.api.csproj --launch-profile mfe_versions.api
```
