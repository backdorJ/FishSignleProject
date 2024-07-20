FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FishShop.API/FishShop.API.csproj", "FishShop.API/"]
COPY ["FishShop.DAL/FishShop.DAL.csproj", "FishShop.DAL/"]
COPY ["FishShop.Core/FishShop.Core.csproj", "FishShop.Core/"]
COPY ["FishShop.Contracts/FishShop.Contracts.csproj", "FishShop.Contracts/"]
RUN dotnet restore "FishShop.API/FishShop.API.csproj"
COPY . .
WORKDIR "/src/FishShop.API"
RUN dotnet build "FishShop.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FishShop.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FishShop.API.dll"]
