@echo off
rem Needs VS2015 installed to build.

set PF=C:\Program Files (x86)\
if not exist "%PF%" (
	set PF=C:\Program Files\
)

set NUNIT=packages\NUnit.ConsoleRunner.3.5.0\tools\nunit3-console.exe

set CONFIG=%1
if "%CONFIG%" == "" set CONFIG=Debug

"%NUNIT%" "OneOf.UnitTests\bin\%CONFIG%\OneOf.UnitTests.dll"
if errorlevel 1 goto :fail
echo.

:success
exit /b 0

:fail
exit /b 1
