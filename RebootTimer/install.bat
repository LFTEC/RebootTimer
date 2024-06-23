@echo off
setlocal

REM check and install .NET Framework 2.0
echo check whether .NET Framework 2.0 is  installed...
reg query "HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727" /v Install >nul 2>&1
if %errorlevel% neq 0 (
    echo .NET Framework 2.0 not exist, installing...
    REM ��ȷ�� .NET Framework ��װ����·����ȷ
    start /wait NetFx20SP1_x86.exe /q /c:"install.exe /q"
    echo .NET Framework 2.0 installed successful.
) else (
    echo .NET Framework 2.0 has already installed.
)

REM ʹ�� installutil ��װ����
echo installing RebootTimer service...
set SERVICE_PATH="C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil.exe"
%SERVICE_PATH% -i RebootTimer.exe
if %errorlevel% neq 0 (
    echo RebootTimer installed failure.
    exit /b %errorlevel%
) else (
    echo RebootTimer installed successful.
)

REM ��������
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