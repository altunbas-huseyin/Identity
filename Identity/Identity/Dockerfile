FROM microsoft/dotnet:1.0.4-core
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Identity.dll"]
#Identity uygulama dosyalar� container i�inde /app yoluna kopyalan�yor.
COPY . /app 
