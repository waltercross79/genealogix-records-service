FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY Genealogix.Records.Api/Genealogix.Records.Api.csproj ./Genealogix.Records.Api/
WORKDIR /app/Genealogix.Records.Api
RUN dotnet restore

# copy everything else and build app
COPY Genealogix.Records.Api/. ./
WORKDIR /app/Genealogix.Records.Api
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/Genealogix.Records.Api/out ./
ENTRYPOINT ["dotnet", "Genealogix.Records.Api.dll"]