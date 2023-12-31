FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src/Services/Blog
COPY ["1-Api/Api/Api.csproj", "src/Services/Blog/1-Api/Api/"]
COPY ["2-Business/Business/Business.csproj", "src/Services/Blog/2-Business/Business/"]
COPY ["3-Data/Data/Data.csproj", "src/Services/Blog/3-Data/Data/"]
RUN dotnet restore "src/Services/Blog/1-Api/Api/Api.csproj"
COPY . .
WORKDIR "/src/Services/Blog/1-Api/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]