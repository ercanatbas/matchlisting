#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update && apt-get install -y libgdiplus

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["service/MatchList.Api/MatchList.Api.csproj", "service/MatchList.Api/"]
COPY ["service/MatchList.Domain/MatchList.Domain.csproj", "service/MatchList.Domain/"]
COPY ["service/MatchList.Application/MatchList.Application.csproj", "service/MatchList.Application/"]
COPY ["service/MatchList.Infrastructure/MatchList.Infrastructure.csproj", "service/MatchList.Infrastructure/"]
RUN dotnet restore "service/MatchList.Api/MatchList.Api.csproj"
COPY . .
WORKDIR "/src/service/MatchList.Api"
RUN dotnet build "MatchList.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MatchList.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MatchList.Api.dll"]