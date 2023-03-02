FROM registry.access.redhat.com/ubi8/dotnet-31:3.1
WORKDIR /server
COPY ./ /server 
ENV PORT=8080
RUN dotnet publish -c Release
EXPOSE 8080

CMD ["dotnet", "./bin/Release/netcoreapp3.0/publish/ComicBookAPI.dll"]
