FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CadContato.WebApi/CadContato.WebApi.csproj", "CadContato.WebApi/"]
COPY ["CadContato.Domain.Infra/CadContato.Domain.Infra.csproj", "CadContato.Domain.Infra/"]
COPY ["CadContato.Domain/CadContato.Domain.csproj", "CadContato.Domain/"]
COPY ["CadContato.Shared/CadContato.Shared.csproj", "CadContato.Shared/"]
RUN dotnet restore "CadContato.WebApi/CadContato.WebApi.csproj"
COPY . .
WORKDIR "/src/CadContato.WebApi"
RUN dotnet build "CadContato.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CadContato.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CadContato.WebApi.dll"]