FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RegistrationApi/RegistrationApi.csproj", "RegistrationApi/"]
COPY ["Infrastrucutre.Buisnes/Infrastrucutre.Buisnes.csproj", "Infrastrucutre.Buisnes/"]
COPY ["Domain.Interfaces/Domain.Interfaces.csproj", "Domain.Interfaces/"]
COPY ["Domain.Dto/Domain.Dto.csproj", "Domain.Dto/"]
COPY ["Infrastructure.Data/Infrastructure.Data.csproj", "Infrastructure.Data/"]
RUN dotnet restore "RegistrationApi/RegistrationApi.csproj"
COPY . .
WORKDIR "/src/RegistrationApi"
RUN dotnet build "RegistrationApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RegistrationApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegistrationApi.dll"]
