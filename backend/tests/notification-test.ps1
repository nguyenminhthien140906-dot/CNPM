param([string]$BaseUrl = "http://localhost:5000")

$pass = 0
$fail = 0

function Check($Name, $Actual, $Expected) {
    if ($Actual -eq $Expected) {
        Write-Host "PASS: $Name ($Actual)" -ForegroundColor Green
        $script:pass++
    }
    else {
        Write-Host "FAIL: $Name (actual=$Actual, expected=$Expected)" -ForegroundColor Red
        $script:fail++
    }
}

function CallApi($Method, $Path, $Token) {
    $headers = @{}

    if ($Token) {
        $headers["Authorization"] = "Bearer $Token"
    }

    try {
        $r = Invoke-WebRequest `
            -Uri "$BaseUrl$Path" `
            -Method $Method `
            -Headers $headers `
            -UseBasicParsing

        return @{
            Code = [int]$r.StatusCode
            Json = if ($r.Content) {
                $r.Content | ConvertFrom-Json
            } else {
                $null
            }
        }
    }
    catch {
        $code = 0

        if ($_.Exception.Response) {
            $code = [int]$_.Exception.Response.StatusCode
        }

        return @{
            Code = $code
            Json = $null
        }
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

Write-Host "== Notification API Tests ==" -ForegroundColor Cyan

$r = CallApi "GET" "/api/notifications" $null
Check "Khong JWT bi chan" $r.Code 401

$r = CallApi "GET" "/api/notifications" $token
Check "Student xem danh sach" $r.Code 200

$r = CallApi "PATCH" "/api/notifications/999999999/read" $token
Check "Thong bao khong ton tai" $r.Code 404

Write-Host ""
Write-Host "Ket qua: $pass PASS, $fail FAIL" -ForegroundColor Cyan

if ($fail -gt 0) { exit 1 }
