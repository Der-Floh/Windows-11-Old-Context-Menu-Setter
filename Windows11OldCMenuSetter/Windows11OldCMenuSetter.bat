@echo off

title Windows 11 Old Context Menu-Setter
echo Checking if Systemversion is Windows 11...
systeminfo | findstr /i /c:"windows 11" > nul && goto :checkexist || goto :wrongos

:wrongos
cls
echo Not on Windows 11.
goto :exit

:checkexist
reg query "HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32" >nul
if %errorlevel% equ 0 (
  goto :choiceR
) else (
  goto :choiceA
)

:choiceR
set /P c=Do you want to Remove the old Menu[Y/N]?
if /I "%c%" equ "Y" goto :remove
if /I "%c%" equ "Yes" goto :remove
if /I "%c%" equ "N" goto :exit
if /I "%c%" equ "No" goto :exit
goto :choiceR

:choiceA
set /P c=Do you want to Add the old Menu[Y/N]?
if /I "%c%" equ "Y" goto :add
if /I "%c%" equ "Yes" goto :add
if /I "%c%" equ "N" goto :exit
if /I "%c%" equ "No" goto :exit
goto :choiceA

:add
reg.exe add "HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32" /f /ve
goto :restartexplorer

:remove
reg.exe delete "HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}" /f
goto :restartexplorer

:restartexplorer
taskkill /f /im explorer.exe
start explorer.exe

:exit
timeout 5
exit