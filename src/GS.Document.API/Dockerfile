#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/GS.Document.API/GS.Document.API.csproj", "src/GS.Document.API/"]
COPY ["src/GS.Document.Domain/GS.Document.Domain.csproj", "src/GS.Document.Domain/"]
COPY ["src/GS.Document.Infra.S3/GS.Document.Infra.S3.csproj", "src/GS.Document.Infra.S3/"]
COPY ["src/GS.Document.Application/GS.Document.Application.csproj", "src/GS.Document.Application/"]
COPY ["src/GS.Document.Infra.Kafka/GS.Document.Infra.Kafka.csproj", "src/GS.Document.Infra.Kafka/"]
COPY ["src/GS.Document.Infra.Database/GS.Document.Infra.Database.csproj", "src/GS.Document.Infra.Database/"]
RUN dotnet restore "src/GS.Document.API/GS.Document.API.csproj"
COPY . .
WORKDIR "/src/src/GS.Document.API"
RUN dotnet build "GS.Document.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GS.Document.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GS.Document.API.dll"]