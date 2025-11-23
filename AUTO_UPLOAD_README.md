# Auto-Upload Agent for C# Learning Notes

This automated agent commits and pushes your daily C# lesson notes to GitHub every 5 minutes.

## Quick Start

### Option 1: Double-click the batch file (Easiest)
Simply double-click `start_auto_upload.bat` to start the agent.

### Option 2: Run directly in PowerShell
```powershell
.\auto_upload.ps1
```

### Option 3: Specify a custom lesson file
```powershell
.\auto_upload.ps1 -LessonFile "2025-11-24_CSharp_Lesson.md"
```

## How It Works

1. **Auto-detection**: The script automatically finds today's lesson file (format: `YYYY-MM-DD_CSharp_Lesson.md`)
2. **Monitoring**: Every 5 minutes, it checks if the lesson file has been updated
3. **Auto-commit**: If changes are detected, it commits them with a timestamped message
4. **Auto-push**: Changes are automatically pushed to GitHub at `raymondnes/csharp-learning-notes`

## Stopping the Agent

Press `Ctrl+C` in the PowerShell window to stop the agent.

## Daily Workflow

### Each Day:
1. When you start your lesson, run `start_auto_upload.bat`
2. The agent will automatically detect today's lesson file
3. Work on your lesson as normal - the agent handles all Git operations
4. When the lesson is done, press `Ctrl+C` to stop the agent

### For the next day:
Simply run the agent again - it will automatically detect the new day's lesson file!

## GitHub Repository

Your notes are being uploaded to:
**https://github.com/raymondnes/csharp-learning-notes**

## Troubleshooting

### "Execution Policy" Error
If you get an execution policy error, run PowerShell as Administrator and execute:
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### No Lesson File Found
Make sure your lesson file follows the naming pattern: `YYYY-MM-DD_CSharp_Lesson.md`

Or specify the file explicitly:
```powershell
.\auto_upload.ps1 -LessonFile "your-lesson-file.md"
```

## Features

- ✅ Auto-detects today's lesson file
- ✅ Commits every 5 minutes if changes detected
- ✅ Automatically pushes to GitHub
- ✅ Colored console output for easy monitoring
- ✅ Error handling and retry logic
- ✅ Works across multiple days automatically
