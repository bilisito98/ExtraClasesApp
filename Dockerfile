# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar el archivo .csproj desde la subcarpeta
COPY ExtraClasesApp/*.csproj ./ExtraClasesApp/
RUN dotnet restore ./ExtraClasesApp/ExtraClasesApp.csproj

# Copiar todo el contenido
COPY . ./
WORKDIR /src/ExtraClasesApp

RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "ExtraClasesApp.dll"]
