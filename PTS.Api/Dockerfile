FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build 
WORKDIR /src
COPY . . 
RUN dotnet restore "PTS.Api.csproj"
RUN dotnet build "PTS.Api.csproj" -c Release -o /app/build/

FROM build AS publish 
WORKDIR /src
RUN dotnet publish "PTS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false 

FROM base AS final 
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "PTS.Api.dll"]
