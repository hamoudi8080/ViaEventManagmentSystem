FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore and build
RUN dotnet restore "src/Presentation/ViaEventManagmentSystem.Presentation.WebAPI/ViaEventManagmentSystem.Presentation.WebAPI.csproj"
RUN dotnet build "src/Presentation/ViaEventManagmentSystem.Presentation.WebAPI/ViaEventManagmentSystem.Presentation.WebAPI.csproj" -c Release -o /app/build

# Publish
RUN dotnet publish "src/Presentation/ViaEventManagmentSystem.Presentation.WebAPI/ViaEventManagmentSystem.Presentation.WebAPI.csproj" -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8080

# Set environment to run on HTTP only
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ViaEventManagmentSystem.Presentation.WebAPI.dll"]