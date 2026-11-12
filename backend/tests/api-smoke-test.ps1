param(
    [string]$BaseUrl = "http://localhost:5000",
    [string]$Password = "Collab@12345"
)

$script:pass = 0
$script:fail = 0

function Call($Method, $Path, $Token, $Body) {
    $headers = @{}
    if ($Token) { $headers["Authorization"] = "Bearer $Token" }

    $params = @{
        Uri             = "$BaseUrl$Path"
        Method          = $Method
        Headers         = $headers
        UseBasicParsing = $true
    }

    if ($Body) {
        $params["Body"] = ($Body | ConvertTo-Json)
        $params["ContentType"] = "application/json"
    }

    $code = 0
    $json = $null

    try {
        $response = Invoke-WebRequest @params
        $code = [int]$response.StatusCode
        if ($response.Content) { $json = $response.Content | ConvertFrom-Json }
    }
    catch {
        if ($_.Exception.Response) { $code = [int]$_.Exception.Response.StatusCode }
    }

    return [pscustomobject]@{ Code = $code; Json = $json }
}

function Check($Name, $Condition) {
    if ($Condition) {
        Write-Host "PASS  $Name" -ForegroundColor Green
        $script:pass++
    }
    else {
        Write-Host "FAIL  $Name" -ForegroundColor Red
        $script:fail++
    }
}

Write-Host "== Kiem tra ket noi ==" -ForegroundColor Cyan
$r = Call "GET" "/api/system/database-check" $null $null
Check "Database ket noi, mapping dung (200)" ($r.Code -eq 200)

Write-Host "== Dang nhap ==" -ForegroundColor Cyan
$r = Call "POST" "/api/auth/login" $null @{ usernameOrEmail = "admin"; password = $Password }
Check "Admin dang nhap (200)" ($r.Code -eq 200)
$admin = $r.Json.data.accessToken

$r = Call "POST" "/api/auth/login" $null @{ usernameOrEmail = "student01"; password = $Password }
Check "Student dang nhap (200)" ($r.Code -eq 200)
$student = $r.Json.data.accessToken

$r = Call "POST" "/api/auth/login" $null @{ usernameOrEmail = "admin"; password = "sai-mat-khau" }
Check "Sai mat khau bi tu choi (401)" ($r.Code -eq 401)

if (-not $admin) {
    Write-Host "Khong lay duoc token admin, dung test." -ForegroundColor Red
    exit 1
}

Write-Host "== Phan quyen ==" -ForegroundColor Cyan
$r = Call "GET" "/api/subjects" $null $null
Check "Khong token bi chan (401)" ($r.Code -eq 401)

$r = Call "POST" "/api/subjects" $student @{ subjectCode = "TESTX"; subjectName = "Test" }
Check "Student khong duoc tao mon (403)" ($r.Code -eq 403)

Write-Host "== Mon hoc ==" -ForegroundColor Cyan
$r = Call "GET" "/api/subjects" $admin $null
Check "Lay danh sach mon co du lieu seed (200)" ($r.Code -eq 200 -and $r.Json.data.Count -gt 0)

$r = Call "POST" "/api/subjects" $admin @{ subjectCode = "TEST01"; subjectName = "Mon test"; description = "tam" }
Check "Tao mon hoc (201)" ($r.Code -eq 201)
$subjectId = $r.Json.data.subjectId

$r = Call "POST" "/api/subjects" $admin @{ subjectCode = "TEST01"; subjectName = "Trung ma" }
Check "Tao mon trung ma bi chan (409)" ($r.Code -eq 409)

$r = Call "PUT" "/api/subjects/$subjectId" $admin @{ subjectCode = "TEST01"; subjectName = "Mon test da sua" }
Check "Sua mon hoc (200)" ($r.Code -eq 200)

$r = Call "GET" "/api/subjects/$subjectId" $admin $null
Check "Lay mon theo id, ten da doi (200)" ($r.Code -eq 200 -and $r.Json.data.subjectName -eq "Mon test da sua")

$r = Call "GET" "/api/subjects/999999" $admin $null
Check "Mon khong ton tai (404)" ($r.Code -eq 404)

Write-Host "== Syllabus ==" -ForegroundColor Cyan
$r = Call "POST" "/api/syllabi" $admin @{ subjectId = $subjectId; version = "v1"; title = "De cuong test"; content = "noi dung" }
Check "Tao syllabus (201)" ($r.Code -eq 201)
$syllabusId = $r.Json.data.syllabusId

$r = Call "POST" "/api/syllabi" $admin @{ subjectId = $subjectId; version = "v1"; title = "Trung phien ban" }
Check "Syllabus trung phien ban bi chan (409)" ($r.Code -eq 409)

$r = Call "POST" "/api/syllabi" $admin @{ subjectId = 999999; version = "v1"; title = "Sai mon" }
Check "Syllabus mon khong ton tai (404)" ($r.Code -eq 404)

$r = Call "GET" "/api/syllabi?subjectId=$subjectId" $admin $null
Check "Loc syllabus theo mon (1 dong)" ($r.Code -eq 200 -and $r.Json.data.Count -eq 1)

Write-Host "== Lop hoc ==" -ForegroundColor Cyan
$r = Call "POST" "/api/classes" $admin @{ subjectId = $subjectId; classCode = "TEST-C1"; className = "Lop test"; semester = "HK1"; academicYear = "2026-2027"; capacity = 1; status = "OPEN" }
Check "Tao lop (201)" ($r.Code -eq 201)
$classId = $r.Json.data.classId

$r = Call "POST" "/api/classes" $admin @{ subjectId = $subjectId; classCode = "TEST-C1"; className = "Trung ma" }
Check "Lop trung ma bi chan (409)" ($r.Code -eq 409)

$r = Call "POST" "/api/classes" $admin @{ subjectId = $subjectId; classCode = "TEST-C2"; className = "Sai status"; status = "ABC" }
Check "Status khong hop le bi chan (400)" ($r.Code -eq 400)

$r = Call "GET" "/api/classes" $admin $null
Check "Lay danh sach lop (200)" ($r.Code -eq 200 -and $r.Json.data.Count -gt 0)

Write-Host "== Phan cong giang vien ==" -ForegroundColor Cyan
$r = Call "GET" "/api/lecturers" $admin $null
Check "Lay danh sach giang vien (200)" ($r.Code -eq 200 -and $r.Json.data.Count -gt 0)
$lecturerId = $r.Json.data[0].lecturerId

$r = Call "POST" "/api/classes/$classId/lecturers" $admin @{ lecturerId = $lecturerId }
Check "Phan cong giang vien (201)" ($r.Code -eq 201)

$r = Call "POST" "/api/classes/$classId/lecturers" $admin @{ lecturerId = $lecturerId }
Check "Phan cong trung bi chan (409)" ($r.Code -eq 409)

$r = Call "GET" "/api/classes/$classId/lecturers" $admin $null
Check "Lop co 1 giang vien" ($r.Code -eq 200 -and $r.Json.data.Count -eq 1)

Write-Host "== Them sinh vien vao lop ==" -ForegroundColor Cyan
$r = Call "GET" "/api/students" $admin $null
Check "Lay danh sach sinh vien, co it nhat 2 (200)" ($r.Code -eq 200 -and $r.Json.data.Count -ge 2)
$studentA = $r.Json.data[0].studentId
$studentB = $r.Json.data[1].studentId

$r = Call "POST" "/api/classes/$classId/students" $admin @{ studentId = $studentA }
Check "Them sinh vien thu 1 (201)" ($r.Code -eq 201)

$r = Call "POST" "/api/classes/$classId/students" $admin @{ studentId = $studentA }
Check "Them sinh vien trung bi chan (409)" ($r.Code -eq 409)

$r = Call "POST" "/api/classes/$classId/students" $admin @{ studentId = $studentB }
Check "Lop day si so (capacity 1) bi chan (409)" ($r.Code -eq 409)

$r = Call "PUT" "/api/classes/$classId" $admin @{ subjectId = $subjectId; classCode = "TEST-C1"; className = "Lop test"; capacity = 0; status = "OPEN" }
Check "Giam capacity thap hon so SV hien tai bi chan (409)" ($r.Code -eq 409)

$r = Call "GET" "/api/classes/$classId/students" $admin $null
Check "Lop co 1 sinh vien" ($r.Code -eq 200 -and $r.Json.data.Count -eq 1)

Write-Host "== Contract moi: PATCH, status, syllabus ==" -ForegroundColor Cyan
$r = Call "PATCH" "/api/classes/$classId" $admin @{ className = "Lop test da patch" }
Check "PATCH mot phan lop (200)" ($r.Code -eq 200)

$r = Call "PATCH" "/api/classes/$classId/status" $admin @{ status = "CLOSED" }
Check "Doi trang thai lop sang CLOSED (200)" ($r.Code -eq 200)

$r = Call "POST" "/api/classes/$classId/students" $admin @{ studentId = $studentB }
Check "Them sinh vien vao lop CLOSED bi chan (409)" ($r.Code -eq 409)

$r = Call "PATCH" "/api/classes/$classId/status" $admin @{ status = "ABC" }
Check "Trang thai khong hop le (400)" ($r.Code -eq 400)

$r = Call "PATCH" "/api/classes/$classId/status" $admin @{ status = "OPEN" }
Check "Mo lai lop (200)" ($r.Code -eq 200)

$r = Call "GET" "/api/subjects/$subjectId/syllabus" $admin $null
Check "Xem de cuong cua mon (200, 1 phien ban)" ($r.Code -eq 200 -and $r.Json.data.syllabi.Count -eq 1)

$r = Call "GET" "/api/subjects/999999/syllabus" $admin $null
Check "De cuong cua mon khong ton tai (404)" ($r.Code -eq 404)

$r = Call "GET" "/api/admin/users?page=1&pageSize=5" $admin $null
Check "Admin xem danh sach nguoi dung (200)" ($r.Code -eq 200)

$r = Call "GET" "/api/admin/users?page=1&pageSize=5" $student $null
Check "Student khong xem duoc danh sach nguoi dung (403)" ($r.Code -eq 403)

Write-Host "== Rang buoc xoa ==" -ForegroundColor Cyan
$r = Call "DELETE" "/api/subjects/$subjectId" $admin $null
Check "Xoa mon dang co lop bi chan (409)" ($r.Code -eq 409)

Write-Host "== Don dep ==" -ForegroundColor Cyan
$r = Call "DELETE" "/api/classes/$classId/students/$studentA" $admin $null
Check "Xoa sinh vien khoi lop (200)" ($r.Code -eq 200)

$r = Call "DELETE" "/api/classes/$classId/lecturers/$lecturerId" $admin $null
Check "Go giang vien khoi lop (200)" ($r.Code -eq 200)

$r = Call "DELETE" "/api/classes/$classId" $admin $null
Check "Xoa lop (200)" ($r.Code -eq 200)

$r = Call "DELETE" "/api/syllabi/$syllabusId" $admin $null
Check "Xoa syllabus (200)" ($r.Code -eq 200)

$r = Call "DELETE" "/api/subjects/$subjectId" $admin $null
Check "Xoa mon hoc (200)" ($r.Code -eq 200)

Write-Host ""
Write-Host "Ket qua: $script:pass PASS, $script:fail FAIL" -ForegroundColor Cyan
if ($script:fail -gt 0) { exit 1 }
