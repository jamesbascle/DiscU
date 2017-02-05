@echo off
rem Needs VS2015 installed to build.

if exist coverage rmdir /s /q coverage

rem ========================
echo Building
echo.

call run-msbuild.bat Debug
if errorlevel 1 goto :fail
echo.

rem ========================
echo Checking coverage
echo.

packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:run-tests.bat -register:user -filter:+[OneOf*]*  -filter:-[OneOf.UnitTests*]* -mergebyhash -safemode:on
if errorlevel 1 goto :fail
echo.

rem ========================
echo Creating report
echo.

packages\ReportGenerator.2.5.2\tools\reportgenerator.exe -reports:results.xml -targetdir:coverage
if errorlevel 1 goto :fail

rem ========================
echo Displaying report
echo.

start coverage\index.htm
if errorlevel 1 goto :fail

:success
exit /b 0

:fail
echo.
echo Failed!
echo.
pause
exit /b 1
