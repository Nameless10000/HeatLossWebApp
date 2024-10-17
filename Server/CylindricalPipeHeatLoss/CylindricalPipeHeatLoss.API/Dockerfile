FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY CylindricalPipeHeatLoss.API/ /src/CylindricalPipeHeatLoss.API
COPY CylindricalPipeHeatLoss.Library/ /src/CylindricalPipeHeatLoss.Library
WORKDIR /src/CylindricalPipeHeatLoss.API
COPY . /src
WORKDIR /src
RUN ls
RUN dotnet restore
RUN dotnet build "./CylindricalPipeHeatLoss.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./CylindricalPipeHeatLoss.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY CylindricalPipeHeatLoss.API/HeatLossDb.db /app/publish/HeatLossDb.db
ENTRYPOINT ["dotnet", "CylindricalPipeHeatLoss.API.dll"]