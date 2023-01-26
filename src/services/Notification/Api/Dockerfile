# .NET 7 Image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app

# Non-root user in order to copy to access /app folder
RUN adduser -u 9999 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

# Expose port for app
EXPOSE 5000

# .NET 7 Image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src

# Copy all needed csproj for build
COPY ["Api/Api.csproj", "Api/"]

RUN dotnet restore "Api/Api.csproj"
COPY . .
WORKDIR /src/Api
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "Api.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Api.dll"]
