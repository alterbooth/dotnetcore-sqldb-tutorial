FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as builder

WORKDIR /workdir

COPY . /workdir

RUN set -ex && \
    dotnet restore DotNetCoreSqlDb.csproj --verbosity Detailed && \
    dotnet build DotNetCoreSqlDb.csproj --configuration Release && \
    dotnet publish DotNetCoreSqlDb.csproj --configuration Release


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /workdir

COPY --from=builder /workdir/bin/Release/netcoreapp2.2/publish .

EXPOSE 80

CMD ["dotnet", "DotNetCoreSqlDb.dll"]
