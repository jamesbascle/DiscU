@echo off
rem Needs VS2015 installed to build.

set PF=C:\Program Files (x86)\
if not exist "%PF%" (
	set PF=C:\Program Files\
)

set MSBUILD=%PF%\MSBuild\14.0\Bin\msbuild.exe
set CONFIG=%1

if "%CONFIG%" == "" set CONFIG=Debug

if exist OneOf\bin del /s /q OneOf\bin >NUL:
if exist OneOf\obj del /s /q OneOf\obj >NUL:
if exist OneOf.UnitTests\bin del /s /q OneOf.UnitTests\bin >NUL:
if exist OneOf.UnitTests\obj del /s /q OneOf.UnitTests\obj >NUL:

"%MSBUILD%" OneOf.sln /nologo /v:m /t:Rebuild /p:Configuration=%CONFIG%
if errorlevel 1 goto :fail

:success
exit /b 0

:fail
echo.
echo Build Failed!
echo.
exit /b 1
