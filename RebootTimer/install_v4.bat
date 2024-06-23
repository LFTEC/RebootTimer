@echo off
setlocal

REM check and install .NET Framework 4.0
echo check whether .NET Framework 4.0 is  installed...
reg query "HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4" /v Install >nul 2>&1
if %errorlevel% neq 0 (
    echo .NET Framework 4.0 not exist, installing...
    REM 请确保 .NET Framework 安装包的路径正确
    start /wait dotNetFx40_Full_x86_x64.exe /q /c:"install.exe /q"
    echo .NET Framework 4.0 installed successful.
) else (
    echo .NET Framework 4.0 has already installed.
)

REM 使用 installutil 安装服务
echo installing RebootTimer service...
set SERVICE_PATH="C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe"
%SERVICE_PATH% -i RebootTimer.exe
if %errorlevel% neq 0 (
    echo RebootTimer installed failure.
    exit /b %errorlevel%
) else (
    echo RebootTimer installed successful.
)

REM 启动服务
echo starting RemoteTimer...
set SERVICE_NAME=RebootTimer
sc start %SERVICE_NAME%
if %errorlevel% neq 0 (
    echo service start error.
    exit /b %errorlevel%
) else (
    echo service started.
)

endlocal
echo all installation successful.
pause