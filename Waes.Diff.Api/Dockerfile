FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Waes.Diff.Api/Waes.Diff.Api.csproj Waes.Diff.Api/
COPY Waes.Diff.Infrastructure/Waes.Diff.Infrastructure.csproj Waes.Diff.Infrastructure/
COPY Waes.Diff.Core/Waes.Diff.Core.csproj Waes.Diff.Core/
RUN dotnet restore Waes.Diff.Api/Waes.Diff.Api.csproj
COPY . .
WORKDIR /src/Waes.Diff.Api
RUN dotnet build Waes.Diff.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Waes.Diff.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Waes.Diff.Api.dll"]
