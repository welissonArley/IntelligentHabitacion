dotnet C:\SonarQube\msbuilder\SonarScanner.MSBuild.dll begin /k:"IntelligentHabitacion" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="KEY" /d:sonar.cs.opencover.reportsPaths=*.Test\coverage.opencover.xml
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet build
dotnet C:\SonarQube\msbuilder\SonarScanner.MSBuild.dll end /d:sonar.login="KEY"