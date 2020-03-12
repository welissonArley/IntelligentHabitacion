C:\SonarQube\msbuilderNetFramework\SonarScanner.MSBuild.exe begin /k:"IntelligentHabitacion" /d:sonar.cs.opencover.reportsPaths=*.Test\coverage.opencover.xml
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" IntelligentHabitacion.sln /t:Rebuild
C:\SonarQube\msbuilderNetFramework\SonarScanner.MSBuild.exe end