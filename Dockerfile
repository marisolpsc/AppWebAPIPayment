FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebAppPayments/WebAppPayments.csproj", "WebAppPayments/"]
RUN dotnet restore "WebAppPayments/WebAppPayments.csproj"
COPY . .
WORKDIR "/src/WebAppPayments"
RUN dotnet build "WebAppPayments.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppPayments.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppPayments.dll"]
