# Auto Upload Script for C# Learning Notes
# This script automatically commits and pushes lesson notes to GitHub every 5 minutes

param(
    [string]$LessonFile = ""
)

# Function to get today's lesson file
function Get-TodayLessonFile {
    $today = Get-Date -Format "yyyy-MM-dd"
    $fileName = "${today}_CSharp_Lesson.md"

    if (Test-Path $fileName) {
        return $fileName
    }

    return $null
}

# Function to commit and push changes
function Sync-ToGitHub {
    param([string]$file)

    $timestamp = Get-Date -Format "HH:mm:ss"

    # Check if there are changes
    git add $file
    $status = git status --porcelain $file

    if ($status) {
        Write-Host "[$timestamp] Changes detected in $file - committing and pushing..." -ForegroundColor Green

        $commitMsg = @"
Update lesson notes: $(Get-Date -Format "yyyy-MM-dd HH:mm")

Automated update of daily C# learning progress.

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude <noreply@anthropic.com>
"@

        git commit -m $commitMsg
        git push origin main

        if ($LASTEXITCODE -eq 0) {
            Write-Host "[$timestamp] Successfully pushed to GitHub!" -ForegroundColor Cyan
        } else {
            Write-Host "[$timestamp] Error pushing to GitHub" -ForegroundColor Red
        }
    } else {
        Write-Host "[$timestamp] No changes detected in $file" -ForegroundColor Yellow
    }
}

# Main loop
Write-Host "==================================================" -ForegroundColor Magenta
Write-Host "C# Lesson Auto-Upload Agent Started" -ForegroundColor Magenta
Write-Host "==================================================" -ForegroundColor Magenta
Write-Host ""

# Determine which lesson file to monitor
if ($LessonFile -eq "") {
    $LessonFile = Get-TodayLessonFile

    if ($null -eq $LessonFile) {
        Write-Host "ERROR: No lesson file found for today. Please specify a lesson file:" -ForegroundColor Red
        Write-Host "Usage: .\auto_upload.ps1 -LessonFile '2025-11-23_CSharp_Lesson.md'" -ForegroundColor Yellow
        exit 1
    }

    Write-Host "Auto-detected lesson file: $LessonFile" -ForegroundColor Green
} else {
    if (-not (Test-Path $LessonFile)) {
        Write-Host "ERROR: Specified lesson file not found: $LessonFile" -ForegroundColor Red
        exit 1
    }
    Write-Host "Using specified lesson file: $LessonFile" -ForegroundColor Green
}

Write-Host "Upload interval: 5 minutes" -ForegroundColor Green
Write-Host "Press Ctrl+C to stop the agent" -ForegroundColor Yellow
Write-Host ""

# Run continuously
$uploadCount = 0
while ($true) {
    try {
        $uploadCount++
        Write-Host "--- Upload cycle #$uploadCount ---" -ForegroundColor Cyan

        Sync-ToGitHub -file $LessonFile

        Write-Host "Waiting 5 minutes until next sync..." -ForegroundColor Gray
        Write-Host ""

        Start-Sleep -Seconds 300  # 5 minutes

    } catch {
        Write-Host "Error occurred: $_" -ForegroundColor Red
        Write-Host "Retrying in 1 minute..." -ForegroundColor Yellow
        Start-Sleep -Seconds 60
    }
}
