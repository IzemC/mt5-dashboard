# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Mt5Dashboard.Web/Mt5Dashboard.Web.csproj"
RUN dotnet publish "Mt5Dashboard.Web/Mt5Dashboard.Web.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Mt5Dashboard.Web.dll"]