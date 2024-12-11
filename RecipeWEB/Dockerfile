FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

EXPOSE 80
ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT Development

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["RecipeWEB/RecipeWEB.csproj", "RecipeWEB/"]
COPY ["DataAccess/DataAccess.csproj", "DaraAccess/"]

RUN dotnet restore "RecipeWEB/RecipeWEB.csproj"

COPY . . 
FROM build AS publish
RUN dotnet publish "RecipeWEB/RecipeWEB.csproj" -c Rwlease -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecipeWEB.dll"]