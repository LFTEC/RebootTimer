@echo off
setlocal

REM 使用 installutil 安装服务
echo uninstalling RebootTimer service...
set SERVICE_PATH="C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe"
%SERVICE_PATH% -u RebootTimer.exe
if %errorlevel% neq 0 (
    echo RebootTimer uninstalled failure.
    exit /b %errorlevel%
) else (
    echo RebootTimer uninstalled successful.
)

endlocal
pause