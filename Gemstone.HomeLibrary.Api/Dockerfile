FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Gemstone.HomeLibrary.Api/Gemstone.HomeLibrary.Api.csproj", "Gemstone.HomeLibrary.Api/"]
COPY ["Gemstone.HomeLibrary.Shared/Gemstone.HomeLibrary.Shared.csproj", "Gemstone.HomeLibrary.Shared/"]
RUN dotnet restore "Gemstone.HomeLibrary.Api/Gemstone.HomeLibrary.Api.csproj"
COPY . .
WORKDIR "/src/Gemstone.HomeLibrary.Api"
RUN dotnet build "Gemstone.HomeLibrary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Gemstone.HomeLibrary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gemstone.HomeLibrary.Api.dll"]
