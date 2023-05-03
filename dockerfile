FROM mcr.microsoft.com/dotnet/sdk:6.0-windowsservercore-ltsc2019 AS build-env

WORKDIR /PBL3/e-commerce-server

COPY *.csproj ./
RUN dotnet restore

COPY . /PBL3/e-commerce-server
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/runtime:6.0-windowsservercore-ltsc2019

WORKDIR /PBL3/e-commerce-server

COPY --from=build-env /PBL3/e-commerce-server/out . 

ENTRYPOINT  ["dotnet", "e-commerce-server"]  



