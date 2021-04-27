#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MassTransit_EF_WebApp/MassTransit_EF_WebApp.csproj", "MassTransit_EF_WebApp/"]
RUN dotnet restore "MassTransit_EF_WebApp/MassTransit_EF_WebApp.csproj"
COPY . .
WORKDIR "/src/MassTransit_EF_WebApp"
RUN dotnet build "MassTransit_EF_WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit_EF_WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit_EF_WebApp.dll"]