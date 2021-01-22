FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#install NodeJS
RUN apt-get update
RUN apt-get -y install nodejs
RUN apt-get -y install npm
RUN npm install -g npm@7
#RUN npm install@latest -g

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
RUN mkdir ./CadContato.Web.Angular
RUN echo '***********************'
COPY ../*.sln .
COPY ./*.csproj ./CadContato.Web.Angular/
COPY . ./CadContato.Web.Angular/
#COPY ./CadContato.Shared/*.csproj .
#COPY ./CadContato.Domain.Infra/*.csproj .
#COPY ./CadContato.Domain/*.csproj .

RUN echo $(ls -la .)
RUN dotnet restore "./CadContato.Web.Angular/CadContato.Web.Angular.csproj"
#COPY . .
#WORKDIR "/src/CadContato.Web.Angular"

RUN dotnet build "./CadContato.Web.Angular/CadContato.Web.Angular.csproj" -c Release -o /app/build


FROM build AS publish
#install NodeJS
#RUN apt-get update
#RUN apt-get -y install nodejs
#RUN apt-get -y install npm
#RUN npm install@latest -g

RUN dotnet publish "./CadContato.Web.Angular/CadContato.Web.Angular.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CadContato.Web.Angular.dll"]