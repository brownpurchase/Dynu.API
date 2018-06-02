@echo Off

set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=0.1.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)


dotnet build Dynu.API.sln -c %config%

cd Dynu.API\bin\Release
@echo %PackageVersion%
nuget pack "Dynu.API.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
