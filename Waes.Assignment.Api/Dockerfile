FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Waes.Assignment.Api/Waes.Assignment.Api.csproj Waes.Assignment.Api/
COPY Waes.Assignment.Infrastructure/Waes.Assignment.Infrastructure.csproj Waes.Assignment.Infrastructure/
COPY Waes.Assignment.Domain/Waes.Assignment.Domain.csproj Waes.Assignment.Domain/
RUN dotnet restore Waes.Assignment.Api/Waes.Assignment.Api.csproj
COPY . .
WORKDIR /src/Waes.Assignment.Api
RUN dotnet build Waes.Assignment.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Waes.Assignment.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Waes.Assignment.Api.dll"]
