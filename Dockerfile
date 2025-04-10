# Fase de construcción - Usa imagen SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia el archivo .csproj y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia el resto del código fuente
COPY . ./

# Publica la aplicación en modo Release
RUN dotnet publish -c Release -o out

# Fase final - Usa imagen distroless de ASP.NET Core Runtime para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled
WORKDIR /app

# Configura el usuario no-root (mejor práctica de seguridad)
USER app

# Variables de entorno para configurar el puerto
ENV ASPNETCORE_URLS=http://+:8080
ENV PORT=8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

# Copia la salida publicada desde la etapa de construcción, cambiando la propiedad al usuario 'app'
COPY --from=build-env --chown=app:app /app/out ./

# Punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "DeudasAPI.dll"]