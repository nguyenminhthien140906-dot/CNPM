param([string]$BaseUrl = "http://localhost:5000")

$ErrorActionPreference = "Stop"
$pass = 0
$fail = 0

function Check($Name, $Actual, $Expected) {
    if ($Actual -eq $Expected) {
        Write-Host "PASS: $Name" -ForegroundColor Green
        $script:pass++
    } else {
        Write-Host "FAIL: $Name (actual=$Actual, expected=$Expected)" -ForegroundColor Red
        $script:fail++
    }
}

function CallApi($Token, $Message) {
    $headers = @{}
    if ($Token) {
        $headers["Authorization"] = "Bearer $Token"
    }

    try {
        $r = Invoke-WebRequest `
            -Uri "$BaseUrl/api/ai/chat" `
            -Method POST `
            -Headers $headers `
            -ContentType "application/json" `
            -Body (@{ message = $Message } | ConvertTo-Json) `
            -UseBasicParsing

        return @{
            Code = [int]$r.StatusCode
            Json = ($r.Content | ConvertFrom-Json)
        }
    }
    catch {
        $code = 0
        if ($_.Exception.Response) {
            $code = [int]$_.Exception.Response.StatusCode
        }
        return @{ Code = $code; Json = $null }
    }
}

if (-not $env:TEST_PASSWORD) {
    throw "Chua co TEST_PASSWORD."
}

$login = Invoke-RestMethod `
    -Uri "$BaseUrl/api/auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body (@{
        usernameOrEmail = "student01"
        password = $env:TEST_PASSWORD
    } | ConvertTo-Json)

$token = $login.data.accessToken

if (-not $token) {
    throw "Khong lay duoc JWT."
}

Write-Host "== AI API Tests ==" -ForegroundColor Cyan

$r = CallApi $null "Xin chao"
Check "Khong JWT bi chan (401)" $r.Code 401

$r = CallApi $token "Xin chao AI"
Check "Tin nhan hop le (200)" $r.Code 200
Check "Flask tra ve success=true" $r.Json.success $true
Check "Flask tra ve noi dung" (-not [string]::IsNullOrWhiteSpace($r.Json.reply)) $true

$r = CallApi $token ""
Check "Tin nhan rong bi chan (400)" $r.Code 400

$r = CallApi $token "   "
Check "Tin nhan chi co khoang trang bi chan (400)" $r.Code 400

Write-Host ""
Write-Host "Ket qua: $pass PASS, $fail FAIL" -ForegroundColor Cyan

if ($fail -gt 0) { exit 1 }
