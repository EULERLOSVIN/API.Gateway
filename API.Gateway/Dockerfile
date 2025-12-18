# Etapa 1: Compilación y Publicación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Al ser un proyecto único, solo necesitamos copiar el .csproj del Gateway
COPY ["API.Gateway/API.Gateway.csproj", "API.Gateway/"]

# Restaurar dependencias
RUN dotnet restore "API.Gateway/API.Gateway.csproj"

# Copiar el resto del código (incluyendo ocelot.json)
COPY . .

# Compilar el proyecto en modo Release
WORKDIR "/src/API.Gateway"
RUN dotnet build "API.Gateway.csproj" -c Release -o /app/build

# Publicar la aplicación optimizada
FROM build AS publish
RUN dotnet publish "API.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Imagen de ejecución (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .

# Comando de inicio usando la DLL del Gateway
ENTRYPOINT ["dotnet", "API.Gateway.dll"]