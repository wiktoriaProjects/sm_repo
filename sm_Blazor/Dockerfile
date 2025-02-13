#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /app
#
#COPY ["sm_Blazor/sm_Blazor.csproj", "sm_Blazor/"]
#RUN dotnet restore "./sm_Blazor/sm_Blazor.csproj"
#COPY . .
#WORKDIR "/src/sm_Blazor"
#RUN dotnet publish "./sm_Blazor.csproj" -c Release -o /app/publish
#
#FROM nginx:alpine AS final
#WORKDIR /usr/share/nginx/html
#COPY --from=build /app/publish/wwwroot .
#
#EXPOSE 80
#COPY sm_Blazor/nginx.conf /etc/nginx/nginx.conf
#COPY --from=build /app/out/wwwroot /usr/share/nginx/html

# Start NGINX
#CMD ["nginx", "-g", "daemon off;"]
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["sm_Blazor/sm_Blazor.csproj", "sm_Blazor/"]
RUN dotnet restore "./sm_Blazor/sm_Blazor.csproj"
COPY . .
WORKDIR "/src/sm_Blazor"
RUN dotnet build "./sm_Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish Stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./sm_Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sm_Blazor.dll"]