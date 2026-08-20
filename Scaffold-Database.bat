@echo off
setlocal

rem Regenerates CourseProject.DataLayer\Models\*.cs and CourseProjectDbContext.cs
rem from the actual schema of CourseProjectDb.mdf (EF Core "Database First").
rem Re-run this any time you change the DB schema by hand.
rem
rem NOTE: if the .mdf is currently open via Visual Studio's Server Explorer /
rem SQL Server Object Explorer, this will fail (file locked) - close that
rem connection in VS first.

set "SolutionDir=%~dp0"
set "DataLayerDir=%SolutionDir%src\CourseProject.DataLayer"
set "MdfPath=%SolutionDir%src\CourseProject.UI\Data\CourseProjectDb.mdf"
set "ConnectionString=Server=(localdb)\MSSQLLocalDB;AttachDbFilename=%MdfPath%;Database=CourseProjectDb;Trusted_Connection=True;TrustServerCertificate=True"

where dotnet-ef >nul 2>nul
if errorlevel 1 (
    echo dotnet-ef tool not found. Install it with:
    echo   dotnet tool install --global dotnet-ef --version 8.0.11
    exit /b 1
)

echo Regenerating EF Core model from %MdfPath% ...

pushd "%DataLayerDir%"
dotnet ef dbcontext scaffold "%ConnectionString%" Microsoft.EntityFrameworkCore.SqlServer -o Models -c CourseProjectDbContext --context-dir . --no-onconfiguring --force
set "ExitCode=%ERRORLEVEL%"
popd

endlocal & exit /b %ExitCode%
