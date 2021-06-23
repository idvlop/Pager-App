#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PagerApp.MVC/PagerApp.MVC.csproj", "PagerApp.MVC/"]
COPY ["PagerApp.IoC/PagerApp.IoC.csproj", "PagerApp.IoC/"]
COPY ["PagerApp.Application/PagerApp.Application.csproj", "PagerApp.Application/"]
COPY ["PagerApp.Domain/PagerApp.Domain.csproj", "PagerApp.Domain/"]
COPY ["PagerApp.Data/PagerApp.Data.csproj", "PagerApp.Data/"]
RUN dotnet restore "PagerApp.MVC/PagerApp.MVC.csproj"
COPY . .
WORKDIR "/src/PagerApp.MVC"
RUN dotnet build "PagerApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PagerApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "PagerApp.MVC.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet PagerApp.MVC.dll