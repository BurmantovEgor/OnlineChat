FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

LABEL maintainer="your_email@example.com"
LABEL description="OnlineChat ASP.NET application"

WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o /app/publish --no-build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 7027

CMD ["dotnet", "OnlineChat.dll"]
