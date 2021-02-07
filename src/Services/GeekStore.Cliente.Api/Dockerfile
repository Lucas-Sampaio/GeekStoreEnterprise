#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["./Services/GeekStore.Cliente.Api/GeekStore.Clientes.Api.csproj", "Services/GeekStore.Cliente.Api/"]
COPY ["./Building Blocks/GeekStore.Core/GeekStore.Core.csproj", "Building Blocks/GeekStore.Core/"]
COPY ["./Building Blocks/Geek.WebApi.Core/Geek.WebApi.Core.csproj", "Building Blocks/Geek.WebApi.Core/"]
COPY ["./Building Blocks/GeekStore.MessageBus/GeekStore.MessageBus.csproj", "Building Blocks/GeekStore.MessageBus/"]
RUN dotnet restore "Services/GeekStore.Cliente.Api/GeekStore.Clientes.Api.csproj"
COPY . .
WORKDIR "Services/GeekStore.Cliente.Api"
RUN dotnet build "GeekStore.Clientes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekStore.Clientes.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekStore.Clientes.Api.dll"]