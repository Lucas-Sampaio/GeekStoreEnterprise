#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["./Web/GeekStore.WebApp.MVC/GeekStore.WebApp.MVC.csproj", "Web/GeekStore.WebApp.MVC/"]
COPY ["./Building Blocks/GeekStore.Core/GeekStore.Core.csproj", "Building Blocks/GeekStore.Core/"]
COPY ["./Building Blocks/Geek.WebApi.Core/Geek.WebApi.Core.csproj", "Building Blocks/Geek.WebApi.Core/"]
RUN dotnet restore "Web/GeekStore.WebApp.MVC/GeekStore.WebApp.MVC.csproj"
COPY . .
WORKDIR "Web/GeekStore.WebApp.MVC"
RUN dotnet build "GeekStore.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekStore.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekStore.WebApp.MVC.dll"]