FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files first for layer caching (restore only re-runs if these change)
COPY WalletApp.sln .
COPY Wallet.API/Wallet.API.csproj Wallet.API/
COPY Wallet.Application/Wallet.Application.csproj Wallet.Application/
COPY Wallet.Domain/Wallet.Domain.csproj Wallet.Domain/
COPY Wallet.Persistence/Wallet.Persistence.csproj Wallet.Persistence/
COPY Wallet.Token/Wallet.Token.csproj Wallet.Token/

RUN dotnet restore

# Copy everything else and publish
COPY . .
RUN dotnet publish Wallet.API/Wallet.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

RUN apt-get update \
    && apt-get install -y --no-install-recommends libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Wallet.API.dll"]
