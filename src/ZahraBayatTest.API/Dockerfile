#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/AliBayatTest.API/AliBayatTest.API.csproj", "src/AliBayatTest.API/"]
COPY ["src/AliBayatTest.Domain/AliBayatTest.Domain.csproj", "src/AliBayatTest.Domain/"]
COPY ["src/AliBayatTest.Infrastructure/AliBayatTest.Infrastructure.csproj", "src/AliBayatTest.Infrastructure/"]
RUN dotnet restore "src/AliBayatTest.API/AliBayatTest.API.csproj"
COPY . .
WORKDIR "/src/src/AliBayatTest.API"
RUN dotnet build "AliBayatTest.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AliBayatTest.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AliBayatTest.API.dll"]
