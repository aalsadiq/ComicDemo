FROM registry.access.redhat.com/ubi8/dotnet-31:3.1
RUN mkdir ComicBookAPI
WORKDIR ComicBookAPI
ADD . .

RUN dotnet publish -c Release

EXPOSE 10000

CMD ["dotnet", "./bin/Release/netcoreapp3.0/publish/ComicBookAPI.dll"]
