FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY . .
RUN dotnet restore IpLookup.sln

# Set environment to release first
ENV ASPNETCORE_ENVIRONMENT Release

# Copy everything else and build
COPY . .
WORKDIR IpProcessorApi
RUN dotnet publish --output /out/ --configuration Release

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /out
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "IpProcessorApi.dll"]