FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SINPRO.csproj", "./"]
RUN dotnet restore "SINPRO.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SINPRO.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SINPRO.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SINPRO.dll"]


# How to setup nginx reverse proxy for aspnet core apps with Docker compose