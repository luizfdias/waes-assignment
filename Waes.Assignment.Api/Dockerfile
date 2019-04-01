FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Waes.Assignment.Api/Waes.Assignment.Api.csproj", "Waes.Assignment.Api/"]
COPY ["Waes.Assignment.Infra/Waes.Assignment.Infra.csproj", "Waes.Assignment.Infra/"]
COPY ["Waes.Assignment.Application/Waes.Assignment.Application.csproj", "Waes.Assignment.Application/"]
COPY ["Waes.Assignment.Domain/Waes.Assignment.Domain.csproj", "Waes.Assignment.Domain/"]
RUN dotnet restore "Waes.Assignment.Api/Waes.Assignment.Api.csproj"
COPY . .
WORKDIR "/src/Waes.Assignment.Api"
RUN dotnet build "Waes.Assignment.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Waes.Assignment.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Waes.Assignment.Api.dll"]