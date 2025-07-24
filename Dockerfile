# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar todo el contenido de una vez (no solo el .csproj)
COPY . ./

# Restaurar dependencias
RUN dotnet restore "./ExtraClasesApp.csproj"

# Publicar en modo Release
RUN dotnet publish "./ExtraClasesApp.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "ExtraClasesApp.dll"]
