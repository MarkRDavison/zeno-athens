FROM mcr.microsoft.com/dotnet/sdk:8.0 as BUILD
WORKDIR /app 

ENV CI_BUILD=true

COPY / /app/
RUN dotnet restore
RUN dotnet publish -c Release -o out api/mark.davison.athens.api/mark.davison.athens.api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled
WORKDIR /app
COPY --from=BUILD /app/out .

ENTRYPOINT ["dotnet", "mark.davison.athens.api.dll"]