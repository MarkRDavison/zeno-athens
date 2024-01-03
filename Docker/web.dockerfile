FROM mcr.microsoft.com/dotnet/sdk:8.0 as BUILD
WORKDIR /app 

ENV CI_BUILD=true

COPY / /app/

RUN dotnet tool install Excubo.WebCompiler --global
RUN /root/.dotnet/tools/webcompiler web/mark.davison.athens.web.ui/compilerconfig.json

RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish/ web/mark.davison.athens.web.ui/mark.davison.athens.web.ui.csproj

FROM nginx:alpine AS FINAL
WORKDIR /usr/share/nginx/html
COPY --from=BUILD /app/publish/wwwroot .
COPY entry.sh /usr/share/nginx/html/entry.sh
COPY nginx.conf /etc/nginx/nginx.conf

RUN chmod +x /usr/share/nginx/html/entry.sh

WORKDIR /usr/share/nginx/html

CMD ["sh", "entry.sh"]