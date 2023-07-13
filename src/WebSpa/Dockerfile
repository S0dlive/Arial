FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WebSpa/WebSpa.csproj", "src/WebSpa/"]
RUN dotnet restore "src/WebSpa/WebSpa.csproj"
COPY . .
WORKDIR "/src/src/WebSpa"
RUN dotnet build "WebSpa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebSpa.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSpa.dll"]
