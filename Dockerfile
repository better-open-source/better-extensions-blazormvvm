ARG DotnetVersion=3.1
FROM mcr.microsoft.com/dotnet/core/sdk:$DotnetVersion-alpine AS build
WORKDIR /app

COPY ./BetterExtensions.BlazorMVVM.sln ./
COPY ./src/BetterExtensions.BlazorMVVM/BetterExtensions.BlazorMVVM.csproj ./src/BetterExtensions.BlazorMVVM/

RUN dotnet restore ./src/BetterExtensions.BlazorMVVM/BetterExtensions.BlazorMVVM.csproj

COPY . ./

ARG CI_BUILDID
ARG CI_PRERELEASE

ENV CI_BUILDID ${CI_BUILDID}
ENV CI_PRERELEASE ${CI_PRERELEASE}

RUN dotnet build ./src/BetterExtensions.BlazorMVVM/BetterExtensions.BlazorMVVM.csproj -c Release --no-restore

RUN dotnet pack ./src/BetterExtensions.BlazorMVVM/BetterExtensions.BlazorMVVM.csproj -c Release --no-restore --no-build -o /app/out