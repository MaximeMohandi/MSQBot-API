FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY *.sln .
COPY MSQBot-API/*.csproj ./MSQBot-API/
COPY MSQBot-API.Business/*.csproj ./MSQBot-API.Business/
COPY MSQBot-API.Core/*.csproj ./MSQBot-API.Core/
COPY MSQBot-API.Infrastructure/*.csproj ./MSQBot-API.Infrastructure/
COPY MSQBot-API.Core.Test/*.csproj ./MSQBot-API.Core.Test/
COPY MSQBot-API.Test/*.csproj ./MSQBot-API.Test/
# Restore as distinct layers
RUN dotnet restore

COPY MSQBot-API/ ./MSQBot-API/
COPY MSQBot-API.Business/ ./MSQBot-API.Business/
COPY MSQBot-API.Core/ ./MSQBot-API.Core/
COPY MSQBot-API.Infrastructure/ ./MSQBot-API.Infrastructure/


WORKDIR /app/MSQBot-API
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 443
COPY --from=build-env /app/MSQBot-API/out .
ENTRYPOINT ["dotnet", "MSQBot-API.dll"]
