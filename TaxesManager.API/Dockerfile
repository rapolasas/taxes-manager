#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaxesManager.API/TaxesManager.API.csproj", "TaxesManager.API/"]
RUN dotnet restore "TaxesManager.API/TaxesManager.API.csproj"
COPY . .
WORKDIR "/src/TaxesManager.API"
RUN dotnet build "TaxesManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaxesManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaxesManager.API.dll"]