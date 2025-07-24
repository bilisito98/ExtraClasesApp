# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiamos todos los archivos del proyecto
COPY . ./

# Restauramos dependencias, especificando el archivo exacto
RUN echo "🧪 Restaurando dependencias..." && dotnet restore "ExtraClasesApp.csproj"

# Publicamos en modo Release
RUN echo "🚀 Publicando aplicación..." && dotnet publish "ExtraClasesApp.csproj" -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /out .

EXPOSE 80

ENTRYPOINT ["dotnet", "ExtraClasesApp.dll"]
