FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /apicatalogo
COPY apicatalogo.csproj.
RUN dotnet restore
COPY . .
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /publish