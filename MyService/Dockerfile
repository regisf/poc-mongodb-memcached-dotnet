FROM mcr.microsoft.com/dotnet/sdk:6.0 as builder

WORKDIR /app
COPY . . 
RUN dotnet build --configuration Release


FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app
COPY . .
COPY --from=builder /app/bin ./bin

CMD ["dotnet", "bin/Release/net6.0/MyService.dll"]

