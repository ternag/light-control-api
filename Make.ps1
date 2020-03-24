dotnet build
dotnet publish --configuration Release --runtime linux-arm --self-contained true src/LightControl.Api/LightControl.Api.csproj
7z a light-control.zip ".\src\LightControl.Api\bin\Release\netcoreapp3.1\linux-arm\publish\*"