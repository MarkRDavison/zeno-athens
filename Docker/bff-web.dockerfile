FROM mcr.microsoft.com/dotnet/sdk:8.0 as BUILD
WORKDIR /app 

COPY / /app/
RUN dotnet restore
RUN dotnet publish -c Release -o out bff-web/mark.davison.athens.bff.web/mark.davison.athens.bff.web.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled
WORKDIR /app
COPY --from=BUILD /app/out .

ENTRYPOINT ["dotnet", "mark.davison.athens.bff.web.dll"]