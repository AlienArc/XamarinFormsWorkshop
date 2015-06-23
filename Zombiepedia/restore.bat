@echo off
cd "%~dp0"
Tools\Nuget\nuget.exe restore ZombiepediaApp\ZombiepediaApp.sln
Tools\Nuget\nuget.exe restore ZombiepediaService\ZombiepediaService.sln
pause