@echo off
rem Needs VS2015 installed to build.

rem ========================
echo Building Release builds.
echo.

call run-msbuild.bat Release
if errorlevel 1 goto :fail
echo.

rem ========================
echo Testing all libraries.
echo.

call run-tests.bat Release
if errorlevel 1 goto :fail
echo.

:success
echo.
echo Success!
echo.
pause
exit /b 0

:fail
echo.
echo Failed!
echo.
pause
exit /b 1
