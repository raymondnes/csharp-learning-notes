@echo off
echo Starting C# Lesson Auto-Upload Agent...
echo.
powershell.exe -ExecutionPolicy Bypass -File "%~dp0auto_upload.ps1"
pause
