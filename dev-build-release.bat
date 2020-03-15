dotnet restore

dotnet build --configuration Debug
dotnet build --configuration Release

dotnet test -c Debug .\tests\TauCode.Domain.Tests\TauCode.Domain.Tests.csproj
dotnet test -c Release .\tests\TauCode.Domain.Tests\TauCode.Domain.Tests.csproj

nuget pack nuget\TauCode.Domain.nuspec
