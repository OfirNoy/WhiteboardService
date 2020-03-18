FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["WhiteboardService.csproj", "WhiteboardService/"]
RUN dotnet restore "WhiteboardService/WhiteboardService.csproj"
WORKDIR "/src/WhiteboardService"
COPY . .
RUN dotnet build "WhiteboardService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhiteboardService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhiteboardService.dll"]