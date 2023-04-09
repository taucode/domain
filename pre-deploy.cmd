dotnet restore

dotnet build TauCode.Domain.sln -c Debug
dotnet build TauCode.Domain.sln -c Release

dotnet test TauCode.Domain.sln -c Debug
dotnet test TauCode.Domain.sln -c Release

nuget pack nuget\TauCode.Domain.nuspec