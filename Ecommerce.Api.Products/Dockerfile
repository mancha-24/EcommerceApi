FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

#ENV ASPNETCORE_URLS=http://+:5059

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ecommerce.Api.Products/Ecommerce.Api.Products.csproj", "Ecommerce.Api.Products/"]
RUN dotnet restore "Ecommerce.Api.Products/Ecommerce.Api.Products.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Api.Products"
RUN dotnet build "Ecommerce.Api.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.Products.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.Products.dll"]
