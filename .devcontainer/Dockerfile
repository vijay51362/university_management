FROM mcr.microsoft.com/dotnet/sdk:9.0

# Install PostgreSQL client for debugging
RUN apt-get update && apt-get install -y postgresql-client

# Set environment variables
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

# Create and set working directory
WORKDIR /workspace

# Install dotnet-ef
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Install ASP.NET Core code generator
RUN dotnet tool install --global dotnet-aspnet-codegenerator