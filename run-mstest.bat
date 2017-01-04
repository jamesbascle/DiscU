@echo off
rem Needs VS2015 installed to build.

set PF=C:\Program Files (x86)\
if not exist "%PF%" (
	set PF=C:\Program Files\
)

set MSTEST=%PF%\Microsoft Visual Studio 14.0\Common7\IDE\mstest.exe

set CONFIG=%1
if "%CONFIG%" == "" set CONFIG=Debug

"%MSTEST%" /testcontainer:"OneOf.UnitTests\bin\%CONFIG%\OneOf.UnitTests.dll" /noresults /nologo
if errorlevel 1 goto :fail
echo.

:success
exit /b 0

:fail
exit /b 1
