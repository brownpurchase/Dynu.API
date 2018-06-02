@echo Off
set version=0.1.0
set configuration=Release
dotnet build Dynu.API.sln -c %configuration%

nuget pack "Dynu.API.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%configuration%"