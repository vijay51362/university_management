FROM mcr.microsoft.com/dotnet/sdk:9.0

# Install PostgreSQL client
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    postgresql-client \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Set environment variables
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true \
    PATH="${PATH}:/root/.dotnet/tools"

# Create and set working directory
WORKDIR /workspace

# Install .NET tools as root (will be available to all users)
RUN dotnet tool install --global dotnet-ef && \
    dotnet tool install --global dotnet-aspnet-codegenerator

# Create vscode user and set permissions
RUN useradd -s /bin/bash -m vscode && \
    chown -R vscode:vscode /workspace