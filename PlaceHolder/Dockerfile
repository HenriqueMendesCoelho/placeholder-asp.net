#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PlaceHolder/PlaceHolder.csproj", "PlaceHolder/"]
RUN dotnet restore "PlaceHolder/PlaceHolder.csproj"
COPY . .
WORKDIR "/src/PlaceHolder"
RUN dotnet build "PlaceHolder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlaceHolder.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "PlaceHolder.dll"]

RUN useradd -m myuserapp
USER myuserapp

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet PlaceHolder.dll