param(
    [string]$BaseUrl = "http://localhost:5000",
    [long]$TeamId = 1
)

$ErrorActionPreference = "Stop"
$pass = 0
$fail = 0
$meetingId = $null

function Check($Name, $Actual, $Expected) {
    if ($Actual -eq $Expected) {
        Write-Host "PASS: $Name ($Actual)" -ForegroundColor Green
        $script:pass++
    } else {
        Write-Host "FAIL: $Name (HTTP $Actual, expected $Expected)" -ForegroundColor Red
        $script:fail++
    }
}

function CallApi($Method, $Path, $Token, $Body = $null) {
    $headers = @{}
    if ($Token) {
        $headers["Authorization"] = "Bearer $Token"
    }

    $params = @{
        Uri = "$BaseUrl$Path"
        Method = $Method
        Headers = $headers
        UseBasicParsing = $true
    }

    if ($null -ne $Body) {
        $params["Body"] = $Body | ConvertTo-Json -Depth 5
        $params["ContentType"] = "application/json"
    }

    try {
        $r = Invoke-WebRequest @params
        $json = $null
        if ($r.Content) {
            $json = $r.Content | ConvertFrom-Json
        }
        return @{ Code = [int]$r.StatusCode; Json = $json }
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
    throw "Chua co TEST_PASSWORD trong Terminal."
}

Write-Host "Chi chay tren database collabsphere_realtime_test!" -ForegroundColor Yellow
$confirm = Read-Host "Da xac nhan backend dung database TEST? (YES)"
if ($confirm -cne "YES") {
    throw "Da huy test."
}

function Login($Username) {
    $r = CallApi "POST" "/api/auth/login" $null @{
        usernameOrEmail = $Username
        password = $env:TEST_PASSWORD
    }

    if ($r.Code -ne 200 -or -not $r.Json.data.accessToken) {
        throw "Dang nhap $Username that bai (HTTP $($r.Code))"
    }

    return $r.Json.data.accessToken
}

$token1 = Login "student01"
$token2 = Login "student02"

Write-Host "== Meeting API Tests ==" -ForegroundColor Cyan

$r = CallApi "GET" "/api/meetings/team/$TeamId" $null
Check "Khong token" $r.Code 401

$r = CallApi "GET" "/api/meetings/team/$TeamId" $token1
Check "Thanh vien xem danh sach" $r.Code 200

$start = (Get-Date).ToUniversalTime().AddDays(1)
$end = $start.AddHours(1)

try {
    $r = CallApi "POST" "/api/meetings" $token1 @{
        teamId = $TeamId
        title = "Meeting automated test"
        description = "Temporary test record"
        meetingUrl = "https://example.com/meeting"
        startsAt = $start.ToString("o")
        endsAt = $end.ToString("o")
    }

    Check "Tao meeting" $r.Code 201

    if ($r.Code -eq 201 -and $r.Json.data.meetingId) {
        $meetingId = $r.Json.data.meetingId

        $r = CallApi "POST" "/api/meetings/$meetingId/join" $token2
        Check "Student02 tham gia meeting" $r.Code 200

        $r = CallApi "PUT" "/api/meetings/$meetingId" $token2 @{
            title = "Unauthorized update"
            startsAt = $start.ToString("o")
            endsAt = $end.ToString("o")
        }
        Check "Student02 khong duoc sua" $r.Code 403

        $r = CallApi "DELETE" "/api/meetings/$meetingId" $token2
        Check "Student02 khong duoc xoa" $r.Code 403

        $r = CallApi "PUT" "/api/meetings/$meetingId" $token1 @{
            title = "Meeting updated"
            description = "Updated by creator"
            meetingUrl = "https://example.com/meeting"
            startsAt = $start.ToString("o")
            endsAt = $end.ToString("o")
        }
        Check "Nguoi tao sua meeting" $r.Code 200
    }
}
finally {
    if ($meetingId) {
        $r = CallApi "DELETE" "/api/meetings/$meetingId" $token1
        Check "Nguoi tao xoa meeting (cleanup)" $r.Code 200
    }
}

Write-Host ""
Write-Host "Ket qua: $pass PASS, $fail FAIL" -ForegroundColor Cyan

if ($fail -gt 0) { exit 1 }
