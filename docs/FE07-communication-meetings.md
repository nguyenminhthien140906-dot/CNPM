<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>CollabSphere - FE03 Quản Lý Lớp Học</title>
    <!-- Tailwind CSS -->
    <script src="https://cdn.tailwindcss.com"></script>
    <!-- Lucide Icons -->
    <script src="https://unpkg.com/lucide@latest"></script>
    <style>
        .custom-scroll::-webkit-scrollbar { width: 6px; height: 6px; }
        .custom-scroll::-webkit-scrollbar-thumb { background: #cbd5e1; border-radius: 4px; }
        .animate-fade-in { animation: fadeIn 0.2s ease-in-out; }
        @keyframes fadeIn { from { opacity: 0; transform: translateY(4px); } to { opacity: 1; transform: translateY(0); } }
    </style>
</head>
<body class="bg-slate-100 font-sans text-slate-800 h-screen overflow-hidden flex flex-col">

    <!-- TOP NAVIGATION & ROLE SWITCHER -->
    <header class="bg-slate-900 text-white px-6 py-3 flex flex-wrap justify-between items-center shadow-md shrink-0 gap-3">
        <div class="flex items-center gap-3">
            <div class="bg-indigo-600 p-2 rounded-lg"><i data-lucide="book-open" class="w-6 h-6 text-white"></i></div>
            <div>
                <h1 class="font-bold text-lg leading-tight">CollabSphere - PBL System</h1>
                <p class="text-xs text-slate-400">Chức năng FE03: Quản lý Lớp học</p>
            </div>
        </div>
        
        <!-- Role Swapper for Simulation (BR03.01 Validation) -->
        <div class="flex items-center gap-3 bg-slate-800 px-3.5 py-1.5 rounded-lg border border-slate-700">
            <i data-lucide="shield-alert" class="w-4 h-4 text-amber-400"></i>
            <span class="text-xs text-slate-300 font-medium">Vai trò truy cập:</span>
            <select id="userRoleSelect" onchange="switchUserRole(this.value)" class="bg-slate-900 text-slate-200 text-sm font-semibold rounded px-2.5 py-1 outline-none border border-slate-700 cursor-pointer focus:ring-2 focus:ring-indigo-500">
                <option value="STAFF">Staff (Nhân viên Quản lý - Toàn quyền)</option>
                <option value="LECTURER">Lecturer (Giảng viên - Chỉ xem lớp phụ trách)</option>
                <option value="STUDENT">Student (Sinh viên - Chỉ xem)</option>
            </select>
        </div>
    </header>

    <!-- NOTIFICATION TOAST -->
    <div id="toastContainer" class="fixed top-16 right-6 z-50 flex flex-col gap-2 pointer-events-none"></div>

    <!-- MAIN APP CONTAINER -->
    <div class="flex flex-1 overflow-hidden">
        
        <!-- LEFT SIDEBAR: CLASS LIST & FILTERS -->
        <aside class="w-80 bg-white border-r border-slate-200 flex flex-col shrink-0">
            <!-- Action Toolbar & Search -->
            <div class="p-4 border-b border-slate-200 space-y-3">
                <div class="flex justify-between items-center">
                    <h2 class="font-bold text-slate-800 text-sm uppercase tracking-wide flex items-center gap-2">
                        <i data-lucide="list" class="w-4 h-4 text-indigo-600"></i> Danh Sách Lớp (<span id="classCount">0</span>)
                    </h2>
                    <div class="flex gap-1">
                        <button onclick="openImportModal()" id="btnImportCsv" title="Nhập CSV (FE03.01)" class="p-1.5 bg-slate-100 hover:bg-slate-200 text-slate-700 rounded-lg transition border border-slate-200">
                            <i data-lucide="file-up" class="w-4 h-4"></i>
                        </button>
                        <button onclick="openClassModal()" id="btnCreateClass" title="Tạo lớp mới (FE03.02)" class="p-1.5 bg-indigo-600 hover:bg-indigo-700 text-white rounded-lg transition shadow-sm">
                            <i data-lucide="plus" class="w-4 h-4"></i>
                        </button>
                    </div>
                </div>

                <!-- Search Input -->
                <div class="relative">
                    <i data-lucide="search" class="w-4 h-4 absolute left-3 top-2.5 text-slate-400"></i>
                    <input type="text" id="searchInput" oninput="filterClasses()" placeholder="Tìm mã lớp, tên lớp, môn..." class="w-full pl-9 pr-3 py-1.5 text-sm bg-slate-50 border border-slate-200 rounded-lg outline-none focus:bg-white focus:border-indigo-500 transition">
                </div>

                <!-- Filters -->
                <div class="grid grid-cols-2 gap-2 text-xs">
                    <select id="semesterFilter" onchange="filterClasses()" class="bg-slate-50 border border-slate-200 rounded p-1.5 text-slate-700 font-medium outline-none">
                        <option value="">Tất cả học kỳ</option>
                        <option value="HK1">Học kỳ 1</option>
                        <option value="HK2">Học kỳ 2</option>
                        <option value="HK3">Học kỳ 3</option>
                    </select>
                    <select id="statusFilter" onchange="filterClasses()" class="bg-slate-50 border border-slate-200 rounded p-1.5 text-slate-700 font-medium outline-none">
                        <option value="">Tất cả trạng thái</option>
                        <option value="ACTIVE">Hoạt động (ACTIVE)</option>
                        <option value="INACTIVE">Ngừng (INACTIVE)</option>
                        <option value="ARCHIVED">Lưu trữ (ARCHIVED)</option>
                    </select>
                </div>
            </div>

            <!-- Class Cards Container -->
            <div id="classListContainer" class="flex-1 overflow-y-auto p-3 space-y-2 custom-scroll">
                <!-- Classes rendered dynamically -->
            </div>
        </aside>

        <!-- RIGHT CONTENT AREA: DETAILED VIEW & TABS -->
        <main class="flex-1 flex flex-col bg-slate-50 overflow-hidden relative" id="mainContentView">
            <!-- Selected Class Details Header -->
            <div id="classHeaderView" class="bg-white border-b border-slate-200 p-6 flex justify-between items-start shadow-sm shrink-0">
                <div>
                    <div class="flex items-center gap-3 mb-1">
                        <span id="detailClassCode" class="font-mono text-lg font-bold bg-indigo-50 text-indigo-700 px-2.5 py-0.5 rounded-md border border-indigo-200">IT001</span>
                        <h2 id="detailClassName" class="text-xl font-bold text-slate-800">Lập trình C++ - Nhóm 1</h2>
                        <span id="detailStatusBadge" class="text-xs px-2.5 py-1 rounded-full font-semibold bg-emerald-100 text-emerald-800">ACTIVE</span>
                    </div>
                    <p class="text-sm text-slate-500 flex items-center gap-4 mt-2">
                        <span><i data-lucide="book" class="w-4 h-4 inline mr-1 text-slate-400"></i>Môn: <strong id="detailSubject" class="text-slate-700">IT101 - Lập trình C++</strong></span>
                        <span><i data-lucide="calendar" class="w-4 h-4 inline mr-1 text-slate-400"></i>Học kỳ: <strong id="detailSemester" class="text-slate-700">HK1 (2026-2027)</strong></span>
                        <span><i data-lucide="clock" class="w-4 h-4 inline mr-1 text-slate-400"></i>Lịch: <strong id="detailSchedule" class="text-slate-700">Thứ 2, Tiết 1-3 (A101)</strong></span>
                    </p>
                </div>

                <!-- Action Controls -->
                <div id="classActionButtons" class="flex items-center gap-2">
                    <button onclick="openEditClassModal()" class="btn-staff-only px-3 py-1.5 bg-slate-100 hover:bg-slate-200 text-slate-700 text-sm font-medium rounded-lg transition border border-slate-200 flex items-center gap-1.5">
                        <i data-lucide="edit-3" class="w-4 h-4"></i> Cập nhật
                    </button>
                    <button onclick="toggleClassStatus()" class="btn-staff-only px-3 py-1.5 bg-amber-50 hover:bg-amber-100 text-amber-700 border border-amber-200 text-sm font-medium rounded-lg transition flex items-center gap-1.5">
                        <i data-lucide="power" class="w-4 h-4"></i> Đổi trạng thái
                    </button>
                </div>
            </div>

            <!-- Tab Navigation Header -->
            <div class="bg-white border-b border-slate-200 px-6 flex gap-6 text-sm font-semibold shrink-0">
                <button onclick="switchMainTab('students')" id="tabStudentsBtn" class="py-3 text-indigo-600 border-b-2 border-indigo-600 flex items-center gap-2">
                    <i data-lucide="users" class="w-4 h-4"></i> Danh sách Sinh viên (<span id="tabStudentCount">0</span>/<span id="detailCapacity">40</span>)
                </button>
                <button onclick="switchMainTab('lecturers')" id="tabLecturersBtn" class="py-3 text-slate-500 hover:text-slate-700 flex items-center gap-2">
                    <i data-lucide="user-check" class="w-4 h-4"></i> Giảng viên phụ trách (<span id="tabLecturerCount">0</span>)
                </button>
            </div>

            <!-- TAB 1: STUDENT MANAGEMENT (FE03.03) -->
            <div id="tabStudentsView" class="flex-1 overflow-y-auto p-6 space-y-4 custom-scroll">
                <!-- Student Toolbar -->
                <div class="flex justify-between items-center bg-white p-4 rounded-xl border border-slate-200 shadow-sm">
                    <div class="flex items-center gap-3">
                        <h3 class="font-bold text-slate-800 text-sm">Thành viên sinh viên trong lớp</h3>
                        <div class="w-48 bg-slate-200 h-2 rounded-full overflow-hidden">
                            <div id="capacityBar" class="bg-indigo-600 h-full transition-all" style="width: 0%;"></div>
                        </div>
                    </div>
                    <div class="flex items-center gap-2">
                        <button onclick="openAddStudentModal()" class="btn-staff-only px-3 py-1.5 bg-indigo-600 hover:bg-indigo-700 text-white text-xs font-semibold rounded-lg shadow-sm flex items-center gap-1">
                            <i data-lucide="user-plus" class="w-3.5 h-3.5"></i> Thêm Sinh Viên
                        </button>
                    </div>
                </div>

                <!-- Student Table -->
                <div class="bg-white rounded-xl border border-slate-200 shadow-sm overflow-hidden">
                    <table class="w-full text-left border-collapse">
                        <thead>
                            <tr class="bg-slate-50 text-xs uppercase font-bold text-slate-500 border-b border-slate-200">
                                <th class="py-3 px-4">Mã SV</th>
                                <th class="py-3 px-4">Họ và Tên</th>
                                <th class="py-3 px-4">Email</th>
                                <th class="py-3 px-4">Chuyên ngành</th>
                                <th class="py-3 px-4 text-center">Thao tác</th>
                            </tr>
                        </thead>
                        <tbody id="studentTableBody" class="divide-y divide-slate-100 text-sm">
                            <!-- Student rows dynamically added -->
                        </tbody>
                    </table>
                </div>
            </div>

            <!-- TAB 2: LECTURER ASSIGNMENT (FE03.04) -->
            <div id="tabLecturersView" class="flex-1 overflow-y-auto p-6 space-y-4 hidden custom-scroll">
                <div class="flex justify-between items-center bg-white p-4 rounded-xl border border-slate-200 shadow-sm">
                    <div>
                        <h3 class="font-bold text-slate-800 text-sm">Giảng viên được phân công</h3>
                        <p class="text-xs text-slate-500">Phân định Giảng viên Chính (Primary) và Giảng viên Hỗ trợ (Assistant)</p>
                    </div>
                    <button onclick="openAssignLecturerModal()" class="btn-staff-only px-3 py-1.5 bg-indigo-600 hover:bg-indigo-700 text-white text-xs font-semibold rounded-lg shadow-sm flex items-center gap-1">
                        <i data-lucide="user-check" class="w-3.5 h-3.5"></i> Phân công Giảng viên
                    </button>
                </div>

                <div class="grid grid-cols-1 md:grid-cols-2 gap-4" id="lecturerCardsContainer">
                    <!-- Lecturer Cards rendered dynamically -->
                </div>
            </div>
        </main>
    </div>

    <!-- MODAL 1: CREATE / EDIT CLASS (FE03.02) -->
    <div id="classModal" class="fixed inset-0 bg-slate-900/60 backdrop-blur-sm flex items-center justify-center hidden z-50">
        <div class="bg-white rounded-2xl w-full max-w-lg p-6 shadow-2xl animate-fade-in">
            <div class="flex justify-between items-center mb-4">
                <h3 id="classModalTitle" class="font-bold text-slate-800 text-lg">Tạo mới Lớp Học</h3>
                <button onclick="closeClassModal()" class="text-slate-400 hover:text-slate-600"><i data-lucide="x" class="w-5 h-5"></i></button>
            </div>
            <form onsubmit="handleSaveClass(event)" class="space-y-3">
                <input type="hidden" id="editClassId">
                <div class="grid grid-cols-2 gap-3">
                    <div>
                        <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Mã lớp * (BR03.02)</label>
                        <input type="text" id="formClassCode" required placeholder="Ví dụ: IT003" class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600">
                    </div>
                    <div>
                        <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Mã môn học * (BR03.03)</label>
                        <select id="formSubjectCode" required class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600 bg-white">
                            <option value="IT101">IT101 - Lập trình C++</option>
                            <option value="IT102">IT102 - Cấu trúc dữ liệu & Giải thuật</option>
                            <option value="SE201">SE201 - Phân tích & Thiết kế Phần mềm</option>
                        </select>
                    </div>
                </div>
                <div>
                    <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Tên lớp học *</label>
                    <input type="text" id="formClassName" required placeholder="Ví dụ: Lập trình C++ - Nhóm 3" class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600">
                </div>
                <div class="grid grid-cols-3 gap-3">
                    <div>
                        <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Học kỳ *</label>
                        <select id="formSemester" required class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600 bg-white">
                            <option value="HK1">HK1</option>
                            <option value="HK2">HK2</option>
                            <option value="HK3">HK3</option>
                        </select>
                    </div>
                    <div>
                        <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Năm học *</label>
                        <input type="text" id="formAcademicYear" required value="2026-2027" class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600">
                    </div>
                    <div>
                        <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Sĩ số tối đa *</label>
                        <input type="number" id="formCapacity" required min="1" max="150" value="40" class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600">
                    </div>
                </div>
                <div class="grid grid-cols-2 gap-3">
                    <div>
                        <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Lịch học</label>
                        <input type="text" id="formSchedule" placeholder="Thứ 3, Tiết 4-6" class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600">
                    </div>
                    <div>
                        <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Phòng học</label>
                        <input type="text" id="formRoom" placeholder="Phòng B202" class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600">
                    </div>
                </div>
                <div class="flex justify-end gap-2 pt-3 border-t border-slate-100">
                    <button type="button" onclick="closeClassModal()" class="px-4 py-2 text-sm font-medium text-slate-600 hover:bg-slate-100 rounded-lg">Hủy</button>
                    <button type="submit" class="px-4 py-2 text-sm font-medium bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 shadow-sm">Lưu thông tin</button>
                </div>
            </form>
        </div>
    </div>

    <!-- MODAL 2: CSV IMPORT PREVIEW & VALIDATION (FE03.01) -->
    <div id="importModal" class="fixed inset-0 bg-slate-900/60 backdrop-blur-sm flex items-center justify-center hidden z-50">
        <div class="bg-white rounded-2xl w-full max-w-3xl p-6 shadow-2xl animate-fade-in flex flex-col max-h-[90vh]">
            <div class="flex justify-between items-center mb-3 shrink-0">
                <div>
                    <h3 class="font-bold text-slate-800 text-lg">Nhập dữ liệu lớp học từ tệp CSV</h3>
                    <p class="text-xs text-slate-500">Kiểm tra định dạng dữ liệu và cảnh báo dòng lỗi trước khi ghi vào cơ sở dữ liệu (BR03.07, BR03.08)</p>
                </div>
                <button onclick="closeImportModal()" class="text-slate-400 hover:text-slate-600"><i data-lucide="x" class="w-5 h-5"></i></button>
            </div>

            <!-- Upload Area / Sample Loader -->
            <div class="p-4 bg-slate-50 rounded-xl border-2 border-dashed border-slate-300 text-center mb-4 shrink-0">
                <i data-lucide="file-spreadsheet" class="w-8 h-8 text-indigo-500 mx-auto mb-1"></i>
                <p class="text-xs text-slate-600 font-medium mb-2">Chọn tệp CSV từ máy tính hoặc tải mẫu kiểm thử bên dưới</p>
                <div class="flex justify-center gap-2">
                    <button onclick="loadSampleCsv('valid')" class="px-3 py-1 bg-emerald-100 text-emerald-800 text-xs font-semibold rounded hover:bg-emerald-200">Nạp mẫu hợp lệ (3 lớp)</button>
                    <button onclick="loadSampleCsv('invalid')" class="px-3 py-1 bg-amber-100 text-amber-800 text-xs font-semibold rounded hover:bg-amber-200">Nạp mẫu có LỖI (Mã trùng/Thiếu trường)</button>
                </div>
            </div>

            <!-- CSV Preview Table -->
            <div id="csvPreviewContainer" class="flex-1 overflow-y-auto border border-slate-200 rounded-xl custom-scroll mb-4 hidden">
                <table class="w-full text-left border-collapse text-xs">
                    <thead class="bg-slate-100 text-slate-600 font-bold sticky top-0 border-b border-slate-200">
                        <tr>
                            <th class="p-2.5">Dòng</th>
                            <th class="p-2.5">Mã Lớp</th>
                            <th class="p-2.5">Tên Lớp</th>
                            <th class="p-2.5">Mã Môn</th>
                            <th class="p-2.5">Học Kỳ</th>
                            <th class="p-2.5">Sĩ Số</th>
                            <th class="p-2.5">Trạng thái Validation</th>
                        </tr>
                    </thead>
                    <tbody id="csvPreviewTableBody" class="divide-y divide-slate-100">
                        <!-- Rendered dynamically -->
                    </tbody>
                </table>
            </div>

            <div class="flex justify-between items-center shrink-0 pt-2 border-t border-slate-100">
                <span id="csvSummaryText" class="text-xs font-medium text-slate-500">Chưa chọn tệp dữ liệu</span>
                <div class="flex gap-2">
                    <button type="button" onclick="closeImportModal()" class="px-4 py-2 text-sm font-medium text-slate-600 hover:bg-slate-100 rounded-lg">Hủy</button>
                    <button type="button" id="btnConfirmImport" onclick="confirmCsvImport()" disabled class="px-4 py-2 text-sm font-medium bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 shadow-sm disabled:opacity-50 disabled:cursor-not-allowed">Xác nhận nhập dữ liệu</button>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL 3: ADD STUDENT TO CLASS (FE03.03) -->
    <div id="addStudentModal" class="fixed inset-0 bg-slate-900/60 backdrop-blur-sm flex items-center justify-center hidden z-50">
        <div class="bg-white rounded-2xl w-full max-w-md p-6 shadow-2xl animate-fade-in">
            <div class="flex justify-between items-center mb-3">
                <h3 class="font-bold text-slate-800 text-lg">Thêm Sinh Viên Vào Lớp</h3>
                <button onclick="closeAddStudentModal()" class="text-slate-400 hover:text-slate-600"><i data-lucide="x" class="w-5 h-5"></i></button>
            </div>
            <form onsubmit="handleAddStudentSubmit(event)" class="space-y-4">
                <div>
                    <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Chọn sinh viên khả dụng trong hệ thống</label>
                    <select id="selectStudentToAdd" required class="w-full border border-slate-300 rounded-lg p-2.5 text-sm outline-none focus:border-indigo-600 bg-white">
                        <!-- Options filled dynamically -->
                    </select>
                </div>
                <div class="p-3 bg-indigo-50 rounded-lg border border-indigo-100 text-xs text-indigo-800 space-y-1">
                    <p class="font-semibold flex items-center gap-1"><i data-lucide="info" class="w-4 h-4"></i> Quy tắc nghiệp vụ kiểm tra:</p>
                    <p>• BR03.04: Tài khoản sinh viên phải hợp lệ.</p>
                    <p>• BR03.05: Không cho phép trùng lặp sinh viên.</p>
                    <p>• BR03.06: Không vượt quá sĩ số tối đa của lớp.</p>
                </div>
                <div class="flex justify-end gap-2 pt-2">
                    <button type="button" onclick="closeAddStudentModal()" class="px-4 py-2 text-sm font-medium text-slate-600 hover:bg-slate-100 rounded-lg">Hủy</button>
                    <button type="submit" class="px-4 py-2 text-sm font-medium bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 shadow-sm">Xác nhận thêm</button>
                </div>
            </form>
        </div>
    </div>

    <!-- MODAL 4: ASSIGN LECTURER (FE03.04) -->
    <div id="assignLecturerModal" class="fixed inset-0 bg-slate-900/60 backdrop-blur-sm flex items-center justify-center hidden z-50">
        <div class="bg-white rounded-2xl w-full max-w-md p-6 shadow-2xl animate-fade-in">
            <div class="flex justify-between items-center mb-3">
                <h3 class="font-bold text-slate-800 text-lg">Phân công Giảng viên</h3>
                <button onclick="closeAssignLecturerModal()" class="text-slate-400 hover:text-slate-600"><i data-lucide="x" class="w-5 h-5"></i></button>
            </div>
            <form onsubmit="handleAssignLecturerSubmit(event)" class="space-y-4">
                <div>
                    <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Chọn Giảng viên</label>
                    <select id="selectLecturerToAssign" required class="w-full border border-slate-300 rounded-lg p-2.5 text-sm outline-none focus:border-indigo-600 bg-white">
                        <!-- Options filled dynamically -->
                    </select>
                </div>
                <div>
                    <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Vai trò phân công</label>
                    <select id="selectAssignmentRole" required class="w-full border border-slate-300 rounded-lg p-2.5 text-sm outline-none focus:border-indigo-600 bg-white">
                        <option value="primary">Giảng viên chính (Primary Lecturer)</option>
                        <option value="assistant">Giảng viên hỗ trợ (Assistant Lecturer)</option>
                    </select>
                </div>
                <div class="flex justify-end gap-2 pt-2">
                    <button type="button" onclick="closeAssignLecturerModal()" class="px-4 py-2 text-sm font-medium text-slate-600 hover:bg-slate-100 rounded-lg">Hủy</button>
                    <button type="submit" class="px-4 py-2 text-sm font-medium bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 shadow-sm">Xác nhận phân công</button>
                </div>
            </form>
        </div>
    </div>

    <!-- APPLICATION LOGIC & STATE ENGINE -->
    <script>
        // System State
        let currentUserRole = "STAFF"; // STAFF, LECTURER, STUDENT
        let selectedClassId = 1;
        let activeTab = "students";
        let parsedImportData = [];

        // Master Mock Database
        let dbSubjects = [
            { code: "IT101", name: "Lập trình C++" },
            { code: "IT102", name: "Cấu trúc dữ liệu & Giải thuật" },
            { code: "SE201", name: "Phân tích & Thiết kế Phần mềm" }
        ];

        let dbUsers = [
            // Students
            { id: 101, name: "Nguyễn Văn A", email: "anguyen@student.edu.vn", role: "STUDENT", major: "Khoa học Máy tính" },
            { id: 102, name: "Trần Thị B", email: "btran@student.edu.vn", role: "STUDENT", major: "Kỹ thuật Phần mềm" },
            { id: 103, name: "Lê Hoàng C", email: "cle@student.edu.vn", role: "STUDENT", major: "Hệ thống Thông tin" },
            { id: 104, name: "Phạm Minh D", email: "dpham@student.edu.vn", role: "STUDENT", major: "Kỹ thuật Phần mềm" },
            { id: 105, name: "Vũ Thanh E", email: "evu@student.edu.vn", role: "STUDENT", major: "Khoa học Máy tính" },
            // Lecturers
            { id: 12, name: "TS. Lê Hoàng", email: "hoang.le@collabsphere.edu.vn", role: "LECTURER", dept: "Bộ môn Công nghệ Phần mềm" },
            { id: 13, name: "ThS. Nguyễn Thị Mai", email: "mai.nguyen@collabsphere.edu.vn", role: "LECTURER", dept: "Bộ môn Khoa học Máy tính" }
        ];

        let dbClasses = [
            {
                id: 1,
                class_code: "IT001",
                class_name: "Lập trình C++ - Nhóm 1",
                subject_code: "IT101",
                semester: "HK1",
                academic_year: "2026-2027",
                capacity: 40,
                schedule: "Thứ 2, tiết 1-3",
                room: "A101",
                status: "ACTIVE",
                student_ids: [101, 102, 103],
                lecturers: [
                    { lecturer_id: 12, assignment_role: "primary" },
                    { lecturer_id: 13, assignment_role: "assistant" }
                ]
            },
            {
                id: 2,
                class_code: "IT002",
                class_name: "Cấu trúc dữ liệu & GT - Nhóm 2",
                subject_code: "IT102",
                semester: "HK1",
                academic_year: "2026-2027",
                capacity: 35,
                schedule: "Thứ 4, tiết 7-9",
                room: "B204",
                status: "ACTIVE",
                student_ids: [104],
                lecturers: [
                    { lecturer_id: 13, assignment_role: "primary" }
                ]
            }
        ];

        // Initialization
        window.addEventListener('DOMContentLoaded', () => {
            renderClassList();
            renderSelectedClassDetails();
            applyAuthorizationRules();
            lucide.createIcons();
        });

        // 1. Authorization Controls (BR03.01)
        function switchUserRole(role) {
            currentUserRole = role;
            applyAuthorizationRules();
            showToast(`Đã chuyển sang vai trò: ${role}`, "info");
        }

        function applyAuthorizationRules() {
            const isStaff = currentUserRole === "STAFF";
            
            // Toggle Visibility of Staff-only mutation actions
            document.querySelectorAll('.btn-staff-only').forEach(el => {
                if (isStaff) {
                    el.classList.remove('hidden');
                } else {
                    el.classList.add('hidden');
                }
            });

            document.getElementById('btnImportCsv').style.display = isStaff ? 'inline-flex' : 'none';
            document.getElementById('btnCreateClass').style.display = isStaff ? 'inline-flex' : 'none';
        }

        // Toast Notification System
        function showToast(message, type = "success") {
            const container = document.getElementById('toastContainer');
            const toast = document.createElement('div');
            
            const bgColor = type === "success" ? "bg-emerald-600" : type === "error" ? "bg-rose-600" : "bg-indigo-600";
            const iconName = type === "success" ? "check-circle" : type === "error" ? "alert-circle" : "info";

            toast.className = `${bgColor} text-white px-4 py-3 rounded-xl shadow-lg flex items-center gap-2.5 text-sm animate-fade-in pointer-events-auto`;
            toast.innerHTML = `<i data-lucide="${iconName}" class="w-4 h-4"></i> <span>${message}</span>`;
            
            container.appendChild(toast);
            lucide.createIcons();

            setTimeout(() => {
                toast.style.opacity = '0';
                toast.style.transition = 'opacity 0.3s ease';
                setTimeout(() => toast.remove(), 300);
            }, 3500);
        }

        // 2. Class List Rendering & Filtering (FE03.02)
        function renderClassList() {
            const container = document.getElementById('classListContainer');
            const searchVal = document.getElementById('searchInput').value.toLowerCase();
            const semesterVal = document.getElementById('semesterFilter').value;
            const statusVal = document.getElementById('statusFilter').value;

            container.innerHTML = '';

            let filtered = dbClasses.filter(c => {
                const matchSearch = c.class_code.toLowerCase().includes(searchVal) ||
                                    c.class_name.toLowerCase().includes(searchVal) ||
                                    c.subject_code.toLowerCase().includes(searchVal);
                const matchSemester = !semesterVal || c.semester === semesterVal;
                const matchStatus = !statusVal || c.status === statusVal;
                
                // Lecturer view filter restriction
                if (currentUserRole === "LECTURER") {
                    const isAssigned = c.lecturers.some(l => l.lecturer_id === 12);
                    return matchSearch && matchSemester && matchStatus && isAssigned;
                }

                return matchSearch && matchSemester && matchStatus;
            });

            document.getElementById('classCount').textContent = filtered.length;

            if (filtered.length === 0) {
                container.innerHTML = `<div class="p-6 text-center text-xs text-slate-400">Không tìm thấy lớp học phù hợp</div>`;
                return;
            }

            filtered.forEach(c => {
                const isSelected = c.id === selectedClassId;
                const card = document.createElement('div');
                card.onclick = () => selectClass(c.id);

                const statusBg = c.status === 'ACTIVE' ? 'bg-emerald-100 text-emerald-800' :
                                 c.status === 'INACTIVE' ? 'bg-amber-100 text-amber-800' : 'bg-slate-200 text-slate-700';

                card.className = `p-3.5 rounded-xl cursor-pointer border transition-all ${
                    isSelected ? 'bg-indigo-50/80 border-indigo-500 shadow-sm' : 'bg-white border-slate-200 hover:border-slate-300'
                }`;

                card.innerHTML = `
                    <div class="flex justify-between items-start mb-1.5">
                        <span class="font-mono text-xs font-bold px-2 py-0.5 rounded ${isSelected ? 'bg-indigo-600 text-white' : 'bg-slate-100 text-slate-700'}">${c.class_code}</span>
                        <span class="text-[10px] px-2 py-0.5 rounded-full font-bold ${statusBg}">${c.status}</span>
                    </div>
                    <h4 class="font-bold text-sm text-slate-800 leading-tight mb-1">${c.class_name}</h4>
                    <div class="flex justify-between items-center text-xs text-slate-500 mt-2">
                        <span>${c.subject_code} • ${c.semester}</span>
                        <span class="font-medium text-slate-600 flex items-center gap-1"><i data-lucide="users" class="w-3 h-3"></i> ${c.student_ids.length}/${c.capacity}</span>
                    </div>
                `;

                container.appendChild(card);
            });

            lucide.createIcons();
        }

        function filterClasses() {
            renderClassList();
        }

        function selectClass(id) {
            selectedClassId = id;
            renderClassList();
            renderSelectedClassDetails();
        }

        // 3. Render Class Detail View
        function renderSelectedClassDetails() {
            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            if (!currentClass) return;

            document.getElementById('detailClassCode').textContent = currentClass.class_code;
            document.getElementById('detailClassName').textContent = currentClass.class_name;
            document.getElementById('detailSubject').textContent = `${currentClass.subject_code} - ${getSubjectName(currentClass.subject_code)}`;
            document.getElementById('detailSemester').textContent = `${currentClass.semester} (${currentClass.academic_year})`;
            document.getElementById('detailSchedule').textContent = `${currentClass.schedule} (${currentClass.room})`;

            const statusBadge = document.getElementById('detailStatusBadge');
            statusBadge.textContent = currentClass.status;
            statusBadge.className = `text-xs px-2.5 py-1 rounded-full font-semibold ${
                currentClass.status === 'ACTIVE' ? 'bg-emerald-100 text-emerald-800' :
                currentClass.status === 'INACTIVE' ? 'bg-amber-100 text-amber-800' : 'bg-slate-200 text-slate-700'
            }`;

            // Render Tabs
            document.getElementById('tabStudentCount').textContent = currentClass.student_ids.length;
            document.getElementById('detailCapacity').textContent = currentClass.capacity;
            document.getElementById('tabLecturerCount').textContent = currentClass.lecturers.length;

            const capPercent = Math.min(100, (currentClass.student_ids.length / currentClass.capacity) * 100);
            const capBar = document.getElementById('capacityBar');
            capBar.style.width = `${capPercent}%`;
            capBar.className = capPercent >= 100 ? 'bg-rose-600 h-full transition-all' : capPercent >= 80 ? 'bg-amber-500 h-full transition-all' : 'bg-indigo-600 h-full transition-all';

            renderStudentsTab();
            renderLecturersTab();
            applyAuthorizationRules();
        }

        function getSubjectName(code) {
            const subj = dbSubjects.find(s => s.code === code);
            return subj ? subj.name : code;
        }

        // Main Tab Switcher
        function switchMainTab(tab) {
            activeTab = tab;
            const btnSt = document.getElementById('tabStudentsBtn');
            const btnLec = document.getElementById('tabLecturersBtn');
            const viewSt = document.getElementById('tabStudentsView');
            const viewLec = document.getElementById('tabLecturersView');

            if (tab === 'students') {
                btnSt.className = "py-3 text-indigo-600 border-b-2 border-indigo-600 flex items-center gap-2";
                btnLec.className = "py-3 text-slate-500 hover:text-slate-700 flex items-center gap-2";
                viewSt.classList.remove('hidden');
                viewLec.classList.add('hidden');
            } else {
                btnLec.className = "py-3 text-indigo-600 border-b-2 border-indigo-600 flex items-center gap-2";
                btnSt.className = "py-3 text-slate-500 hover:text-slate-700 flex items-center gap-2";
                viewLec.classList.remove('hidden');
                viewSt.classList.add('hidden');
            }
        }

        // 4. Student Management Tab (FE03.03)
        function renderStudentsTab() {
            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            const tbody = document.getElementById('studentTableBody');
            tbody.innerHTML = '';

            if (currentClass.student_ids.length === 0) {
                tbody.innerHTML = `<tr><td colspan="5" class="py-6 text-center text-slate-400 text-xs">Chưa có sinh viên nào trong lớp học này.</td></tr>`;
                return;
            }

            currentClass.student_ids.forEach(stId => {
                const student = dbUsers.find(u => u.id === stId);
                if (!student) return;

                const tr = document.createElement('tr');
                tr.className = "hover:bg-slate-50/80 transition";
                tr.innerHTML = `
                    <td class="py-3 px-4 font-mono text-xs font-bold text-slate-700">${student.id}</td>
                    <td class="py-3 px-4 font-semibold text-slate-800">${student.name}</td>
                    <td class="py-3 px-4 text-slate-600 text-xs">${student.email}</td>
                    <td class="py-3 px-4 text-slate-600 text-xs">${student.major}</td>
                    <td class="py-3 px-4 text-center">
                        <button onclick="removeStudentFromClass(${student.id})" class="btn-staff-only p-1 text-rose-600 hover:bg-rose-50 rounded transition" title="Xóa sinh viên khỏi lớp">
                            <i data-lucide="user-x" class="w-4 h-4"></i>
                        </button>
                    </td>
                `;
                tbody.appendChild(tr);
            });

            lucide.createIcons();
        }

        function openAddStudentModal() {
            if (currentUserRole !== "STAFF") {
                showToast("Bạn không có quyền thực hiện thao tác này (BR03.01)", "error");
                return;
            }

            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            
            // Check Capacity Rule (BR03.06)
            if (currentClass.student_ids.length >= currentClass.capacity) {
                showToast(`Lớp đã đạt sĩ số tối đa (${currentClass.capacity} SV). Không thể thêm mới! (BR03.06)`, "error");
                return;
            }

            const select = document.getElementById('selectStudentToAdd');
            select.innerHTML = '';

            // Filter available students (not already in class - BR03.05)
            const availableStudents = dbUsers.filter(u => u.role === "STUDENT" && !currentClass.student_ids.includes(u.id));

            if (availableStudents.length === 0) {
                select.innerHTML = `<option value="">Tất cả sinh viên đã có mặt trong lớp</option>`;
            } else {
                availableStudents.forEach(st => {
                    select.innerHTML += `<option value="${st.id}">${st.id} - ${st.name} (${st.major})</option>`;
                });
            }

            document.getElementById('addStudentModal').classList.remove('hidden');
        }

        function closeAddStudentModal() {
            document.getElementById('addStudentModal').classList.add('hidden');
        }

        function handleAddStudentSubmit(e) {
            e.preventDefault();
            const stId = parseInt(document.getElementById('selectStudentToAdd').value);
            if (!stId) return;

            const currentClass = dbClasses.find(c => c.id === selectedClassId);

            // Double check BR03.05 Duplicate check
            if (currentClass.student_ids.includes(stId)) {
                showToast("Sinh viên này đã thuộc lớp học! (BR03.05)", "error");
                return;
            }

            currentClass.student_ids.push(stId);
            renderSelectedClassDetails();
            renderClassList();
            closeAddStudentModal();
            showToast("Thêm sinh viên vào lớp thành công!");
        }

        function removeStudentFromClass(stId) {
            if (currentUserRole !== "STAFF") {
                showToast("Bạn không có quyền xóa sinh viên (BR03.01)", "error");
                return;
            }

            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            currentClass.student_ids = currentClass.student_ids.filter(id => id !== stId);
            
            renderSelectedClassDetails();
            renderClassList();
            showToast("Đã xóa sinh viên khỏi danh sách lớp!");
        }

        // 5. Lecturer Assignment Tab (FE03.04)
        function renderLecturersTab() {
            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            const container = document.getElementById('lecturerCardsContainer');
            container.innerHTML = '';

            if (currentClass.lecturers.length === 0) {
                container.innerHTML = `<div class="col-span-2 p-6 text-center text-slate-400 text-xs bg-white rounded-xl border border-slate-200">Chưa phân công giảng viên nào cho lớp này.</div>`;
                return;
            }

            currentClass.lecturers.forEach(lecRef => {
                const lecturer = dbUsers.find(u => u.id === lecRef.lecturer_id);
                if (!lecturer) return;

                const isPrimary = lecRef.assignment_role === 'primary';
                const card = document.createElement('div');
                card.className = "bg-white p-4 rounded-xl border border-slate-200 shadow-sm flex justify-between items-center";

                card.innerHTML = `
                    <div class="flex items-center gap-3">
                        <div class="w-10 h-10 rounded-full ${isPrimary ? 'bg-indigo-600' : 'bg-slate-700'} text-white flex items-center justify-center font-bold text-sm">
                            ${lecturer.name.charAt(0)}
                        </div>
                        <div>
                            <div class="flex items-center gap-2">
                                <h4 class="font-bold text-slate-800 text-sm">${lecturer.name}</h4>
                                <span class="text-[10px] px-2 py-0.5 rounded-full font-bold uppercase ${isPrimary ? 'bg-indigo-100 text-indigo-700 border border-indigo-200' : 'bg-slate-100 text-slate-600'}">
                                    ${isPrimary ? 'Giảng viên chính' : 'Giảng viên hỗ trợ'}
                                </span>
                            </div>
                            <p class="text-xs text-slate-500">${lecturer.email} • ${lecturer.dept}</p>
                        </div>
                    </div>
                    <button onclick="removeLecturerFromClass(${lecturer.id})" class="btn-staff-only p-1.5 text-rose-600 hover:bg-rose-50 rounded-lg transition" title="Hủy phân công">
                        <i data-lucide="trash-2" class="w-4 h-4"></i>
                    </button>
                `;
                container.appendChild(card);
            });

            lucide.createIcons();
        }

        function openAssignLecturerModal() {
            if (currentUserRole !== "STAFF") {
                showToast("Bạn không có quyền phân công giảng viên (BR03.01)", "error");
                return;
            }

            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            const select = document.getElementById('selectLecturerToAssign');
            select.innerHTML = '';

            const assignedIds = currentClass.lecturers.map(l => l.lecturer_id);
            const availableLecturers = dbUsers.filter(u => u.role === "LECTURER" && !assignedIds.includes(u.id));

            if (availableLecturers.length === 0) {
                select.innerHTML = `<option value="">Tất cả giảng viên đã được phân công</option>`;
            } else {
                availableLecturers.forEach(l => {
                    select.innerHTML += `<option value="${l.id}">${l.name} (${l.dept})</option>`;
                });
            }

            document.getElementById('assignLecturerModal').classList.remove('hidden');
        }

        function closeAssignLecturerModal() {
            document.getElementById('assignLecturerModal').classList.add('hidden');
        }

        function handleAssignLecturerSubmit(e) {
            e.preventDefault();
            const lecId = parseInt(document.getElementById('selectLecturerToAssign').value);
            const role = document.getElementById('selectAssignmentRole').value;
            if (!lecId) return;

            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            currentClass.lecturers.push({ lecturer_id: lecId, assignment_role: role });

            renderSelectedClassDetails();
            closeAssignLecturerModal();
            showToast("Phân công giảng viên thành công!");
        }

        function removeLecturerFromClass(lecId) {
            if (currentUserRole !== "STAFF") {
                showToast("Bạn không có quyền thao tác (BR03.01)", "error");
                return;
            }

            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            currentClass.lecturers = currentClass.lecturers.filter(l => l.lecturer_id !== lecId);

            renderSelectedClassDetails();
            showToast("Đã hủy phân công giảng viên!");
        }

        // 6. Manual Class Creation / Editing (FE03.02)
        function openClassModal(editData = null) {
            if (currentUserRole !== "STAFF") {
                showToast("Chỉ Nhân viên Quản lý mới có quyền tạo/sửa lớp (BR03.01)", "error");
                return;
            }

            document.getElementById('classModalTitle').textContent = editData ? "Cập Nhật Thông Tin Lớp Học" : "Tạo Mới Lớp Học";
            document.getElementById('editClassId').value = editData ? editData.id : "";
            document.getElementById('formClassCode').value = editData ? editData.class_code : "";
            document.getElementById('formClassCode').disabled = !!editData; // Code is unique & key
            document.getElementById('formClassName').value = editData ? editData.class_name : "";
            document.getElementById('formSubjectCode').value = editData ? editData.subject_code : "IT101";
            document.getElementById('formSemester').value = editData ? editData.semester : "HK1";
            document.getElementById('formAcademicYear').value = editData ? editData.academic_year : "2026-2027";
            document.getElementById('formCapacity').value = editData ? editData.capacity : 40;
            document.getElementById('formSchedule').value = editData ? editData.schedule : "";
            document.getElementById('formRoom').value = editData ? editData.room : "";

            document.getElementById('classModal').classList.remove('hidden');
        }

        function openEditClassModal() {
            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            openClassModal(currentClass);
        }

        function closeClassModal() {
            document.getElementById('classModal').classList.add('hidden');
        }

        function handleSaveClass(e) {
            e.preventDefault();
            const editId = document.getElementById('editClassId').value;
            const code = document.getElementById('formClassCode').value.trim();

            // Unique Check for class_code (BR03.02)
            if (!editId) {
                const isDuplicate = dbClasses.some(c => c.class_code.toLowerCase() === code.toLowerCase());
                if (isDuplicate) {
                    showToast(`Mã lớp '${code}' đã tồn tại trong hệ thống! (BR03.02)`, "error");
                    return;
                }
            }

            if (editId) {
                // Update
                const current = dbClasses.find(c => c.id === parseInt(editId));
                current.class_name = document.getElementById('formClassName').value;
                current.subject_code = document.getElementById('formSubjectCode').value;
                current.semester = document.getElementById('formSemester').value;
                current.academic_year = document.getElementById('formAcademicYear').value;
                current.capacity = parseInt(document.getElementById('formCapacity').value);
                current.schedule = document.getElementById('formSchedule').value;
                current.room = document.getElementById('formRoom').value;
                showToast("Cập nhật thông tin lớp học thành công!");
            } else {
                // Insert
                const newClass = {
                    id: Date.now(),
                    class_code: code,
                    class_name: document.getElementById('formClassName').value,
                    subject_code: document.getElementById('formSubjectCode').value,
                    semester: document.getElementById('formSemester').value,
                    academic_year: document.getElementById('formAcademicYear').value,
                    capacity: parseInt(document.getElementById('formCapacity').value),
                    schedule: document.getElementById('formSchedule').value,
                    room: document.getElementById('formRoom').value,
                    status: "ACTIVE",
                    student_ids: [],
                    lecturers: []
                };
                dbClasses.unshift(newClass);
                selectedClassId = newClass.id;
                showToast("Tạo lớp học mới thành công!");
            }

            renderClassList();
            renderSelectedClassDetails();
            closeClassModal();
        }

        function toggleClassStatus() {
            if (currentUserRole !== "STAFF") {
                showToast("Bạn không có quyền đổi trạng thái lớp (BR03.01)", "error");
                return;
            }

            const currentClass = dbClasses.find(c => c.id === selectedClassId);
            if (currentClass.status === "ACTIVE") currentClass.status = "INACTIVE";
            else if (currentClass.status === "INACTIVE") currentClass.status = "ARCHIVED";
            else currentClass.status = "ACTIVE";

            renderSelectedClassDetails();
            renderClassList();
            showToast(`Đã đổi trạng thái lớp sang: ${currentClass.status}`);
        }

        // 7. CSV File Import Preview & Validation (FE03.01)
        function openImportModal() {
            if (currentUserRole !== "STAFF") {
                showToast("Chỉ Staff mới có quyền nhập dữ liệu từ CSV (BR03.01)", "error");
                return;
            }
            parsedImportData = [];
            document.getElementById('csvPreviewContainer').classList.add('hidden');
            document.getElementById('btnConfirmImport').disabled = true;
            document.getElementById('csvSummaryText').textContent = "Chưa chọn tệp dữ liệu";
            document.getElementById('importModal').classList.remove('hidden');
        }

        function closeImportModal() {
            document.getElementById('importModal').classList.add('hidden');
        }

        function loadSampleCsv(type) {
            let sampleRows = [];
            if (type === 'valid') {
                sampleRows = [
                    { row: 1, class_code: "IT003", class_name: "Lập trình C++ - Nhóm 3", subject_code: "IT101", semester: "HK1", capacity: 40 },
                    { row: 2, class_code: "IT004", class_name: "Cấu trúc dữ liệu - Nhóm 1", subject_code: "IT102", semester: "HK1", capacity: 45 },
                    { row: 3, class_code: "SE001", class_name: "Phân tích phần mềm - Nhóm 1", subject_code: "SE201", semester: "HK2", capacity: 30 }
                ];
            } else {
                // Invalid data samples triggering BR03.02, BR03.03, etc.
                sampleRows = [
                    { row: 1, class_code: "IT001", class_name: "Lớp Trùng Mã", subject_code: "IT101", semester: "HK1", capacity: 40 }, // Duplicate class_code
                    { row: 2, class_code: "IT005", class_name: "Lớp Mã Môn Lỗi", subject_code: "INVALID_SUBJ", semester: "HK1", capacity: 40 }, // Invalid Subject
                    { row: 3, class_code: "", class_name: "Lớp Thiếu Mã Lớp", subject_code: "IT102", semester: "HK1", capacity: 40 } // Missing Code
                ];
            }

            validateAndPreviewCsvData(sampleRows);
        }

        function validateAndPreviewCsvData(rows) {
            parsedImportData = [];
            let validCount = 0;
            let errorCount = 0;

            const tbody = document.getElementById('csvPreviewTableBody');
            tbody.innerHTML = '';

            rows.forEach(r => {
                let errors = [];

                // Check required fields
                if (!r.class_code) errors.push("Thiếu mã lớp");
                if (!r.class_name) errors.push("Thiếu tên lớp");
                
                // Check duplicate code (BR03.02)
                if (r.class_code && dbClasses.some(c => c.class_code.toLowerCase() === r.class_code.toLowerCase())) {
                    errors.push("Mã lớp bị trùng trong hệ thống");
                }

                // Check subject code exists (BR03.03)
                if (r.subject_code && !dbSubjects.some(s => s.code === r.subject_code)) {
                    errors.push("Mã môn học không tồn tại");
                }

                const isValid = errors.length === 0;
                if (isValid) validCount++; else errorCount++;

                parsedImportData.push({ ...r, isValid, errors });

                const tr = document.createElement('tr');
                tr.className = isValid ? "bg-emerald-50/50 hover:bg-emerald-50" : "bg-rose-50/60 hover:bg-rose-50";
                tr.innerHTML = `
                    <td class="p-2.5 font-bold text-slate-500">${r.row}</td>
                    <td class="p-2.5 font-mono font-bold">${r.class_code || '<span class="text-rose-500">N/A</span>'}</td>
                    <td class="p-2.5 font-medium">${r.class_name}</td>
                    <td class="p-2.5">${r.subject_code}</td>
                    <td class="p-2.5">${r.semester}</td>
                    <td class="p-2.5">${r.capacity}</td>
                    <td class="p-2.5 font-medium">
                        ${isValid 
                            ? `<span class="text-emerald-700 flex items-center gap-1"><i data-lucide="check" class="w-3.5 h-3.5"></i> Hợp lệ</span>` 
                            : `<span class="text-rose-700 font-semibold flex items-center gap-1"><i data-lucide="alert-triangle" class="w-3.5 h-3.5"></i> Lỗi: ${errors.join(', ')}</span>`}
                    </td>
                `;
                tbody.appendChild(tr);
            });

            document.getElementById('csvPreviewContainer').classList.remove('hidden');
            document.getElementById('csvSummaryText').innerHTML = `Tổng dòng: <strong>${rows.length}</strong> | Hợp lệ: <strong class="text-emerald-600">${validCount}</strong> | Lỗi: <strong class="text-rose-600">${errorCount}</strong>`;
            
            // Only enable import if valid items exist
            document.getElementById('btnConfirmImport').disabled = validCount === 0;
            lucide.createIcons();
        }

        function confirmCsvImport() {
            const validRows = parsedImportData.filter(d => d.isValid);
            if (validRows.length === 0) return;

