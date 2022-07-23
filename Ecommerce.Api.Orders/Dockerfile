FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5059

ENV ASPNETCORE_URLS=http://+:5059

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ecommerce.Api.Orders/Ecommerce.Api.Orders.csproj", "Ecommerce.Api.Orders/"]
RUN dotnet restore "Ecommerce.Api.Orders\Ecommerce.Api.Orders.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Api.Orders"
RUN dotnet build "Ecommerce.Api.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.Orders.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.Orders.dll"]
