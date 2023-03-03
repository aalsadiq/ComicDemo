# 1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./Comic.csproj" --disable-parallel
RUN dotnet publish "./Comic.csproj" -c Release -o /app --no-restore

# 2
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

CMD ["dotnet", "Comic.dll"]
