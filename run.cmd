@echo off
set "APP_EXE=%~dp0publish\PrimeVideoSpeedApp.exe"

if exist "%APP_EXE%" (
  "%APP_EXE%"
) else (
  dotnet run --project "%~dp0PrimeVideoSpeedApp.csproj"
)
