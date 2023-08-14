FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["FirstDemo.Web/*.csproj", "FirstDemo.Web/"]
COPY ["FirstDemo.Infrastructure/*.csproj", "FirstDemo.Infrastructure/"]
RUN dotnet restore "FirstDemo.Web/FirstDemo.Web.csproj"
COPY . .
WORKDIR "/src/FirstDemo.Web"
RUN dotnet build "FirstDemo.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FirstDemo.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FirstDemo.Web.dll"]