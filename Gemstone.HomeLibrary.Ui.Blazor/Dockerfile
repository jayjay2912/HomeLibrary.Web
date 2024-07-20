FROM --platform=$BUILDPLATFORM nginx:alpine AS base
WORKDIR /app
EXPOSE 8080

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Gemstone.HomeLibrary.Ui.Blazor/Gemstone.HomeLibrary.Ui.Blazor.csproj", "Gemstone.HomeLibrary.Ui.Blazor/"]
COPY ["Gemstone.HomeLibrary.Shared/Gemstone.HomeLibrary.Shared.csproj", "Gemstone.HomeLibrary.Shared/"]
RUN dotnet restore "Gemstone.HomeLibrary.Ui.Blazor/Gemstone.HomeLibrary.Ui.Blazor.csproj"
COPY . .
WORKDIR "/src/Gemstone.HomeLibrary.Ui.Blazor"
RUN dotnet build "Gemstone.HomeLibrary.Ui.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Gemstone.HomeLibrary.Ui.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish .
COPY ["Gemstone.HomeLibrary.Ui.Blazor/nginx.conf", "/etc/nginx/nginx.conf"]