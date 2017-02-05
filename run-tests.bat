@echo off
rem Needs VS2015 installed to build.

set PF=C:\Program Files (x86)\
if not exist "%PF%" (
	set PF=C:\Program Files\
)

set NUNIT=packages\NUnit.ConsoleRunner.3.5.0\tools\nunit3-console.exe

set CONFIG=%1
if "%CONFIG%" == "" set CONFIG=Debug

if exist results.xml del results.xml
if exist TestResult.xml del TestResult.xml

"%NUNIT%" "OneOf.UnitTests\bin\%CONFIG%\OneOf.UnitTests.dll" /config:%CONFIG% --agents=1
if errorlevel 1 goto :fail
echo.

:success
exit /b 0

:fail
exit /b 1
