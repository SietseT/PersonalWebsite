FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base

EXPOSE 80
EXPOSE 443
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build

ENV ASPNETCORE_URLS=http://+:5000

WORKDIR /usr/src/api

COPY *.csproj ./
RUN dotnet restore

COPY . ./
WORKDIR /usr/src/api

FROM build AS publish
RUN dotnet publish -c Release -o publish

FROM base AS final
WORKDIR /usr/src/api
COPY --from=publish /usr/src/api/publish .
 
ENTRYPOINT ["dotnet", "Har.dll"]