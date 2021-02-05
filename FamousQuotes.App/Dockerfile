#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FamousQuotes.App/FamousQuotes.App.csproj", "FamousQuotes.App/"]
RUN dotnet restore "FamousQuotes.App/FamousQuotes.App.csproj"
COPY . .
WORKDIR "/src/FamousQuotes.App"
RUN dotnet build "FamousQuotes.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FamousQuotes.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FamousQuotes.App.dll"]