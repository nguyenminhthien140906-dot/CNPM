
import apiClient from "./apiClient";

import {
    UserDto,
    SubjectDto,
    SyllabusDto,
    ClassDto,
    ProjectDto
} from "./ApiModels";

class CollabSphereApiService {

    // 1. USERS (Admin)

    async getUsers() {
        try {
            const res = await apiClient.get("/users");

            return res
                ? res.map(user => new UserDto(user))
                : this.getMockUsers();
        } catch {
            return this.getMockUsers();
        }
    }

    async createUser(user) {
        try {
            await apiClient.post(
                "/users",
                new UserDto(user)
            );

            return true;
        } catch (error) {
            console.error("Create user failed:", error);
            return false;
        }
    }

    async updateUser(user) {
        try {
            await apiClient.put(
                `/users/${user.id}`,
                user
            );

            return true;
        } catch (error) {
            console.error("Update user failed:", error);
            return false;
        }
    }

    async toggleUserStatus(id, isActive) {
        try {
            await apiClient.patch(
                `/users/${id}/status`,
                { isActive }
            );

            return true;
        } catch (error) {
            console.error("Toggle user status failed:", error);
            return false;
        }
    }

    // 2. SUBJECTS (Môn học)

    async getSubjects() {
        try {
            const res = await apiClient.get("/subjects");

            return res
                ? res.map(subject => new SubjectDto(subject))
                : this.getMockSubjects();
        } catch {
            return this.getMockSubjects();
        }
    }

    async saveSubject(subject, isNew) {
        try {
            if (isNew) {
                await apiClient.post(
                    "/subjects",
                    subject
                );
            } else {
                await apiClient.put(
                    `/subjects/${subject.id}`,
                    subject
                );
            }

            return true;
        } catch (error) {
            console.error("Save subject failed:", error);
            return false;
        }
    }

    async deleteSubject(id) {
        try {
            await apiClient.delete(`/subjects/${id}`);
            return true;
        } catch (error) {
            console.error("Delete subject failed:", error);
            return false;
        }
    }

    // 3. SYLLABUS (Đề cương)

    async getSyllabuses() {
        try {
            const res = await apiClient.get("/syllabuses");

            return res
                ? res.map(item => new SyllabusDto(item))
                : this.getMockSyllabuses();
        } catch {
            return this.getMockSyllabuses();
        }
    }

    async saveSyllabus(syllabus, isNew) {
        try {
            if (isNew) {
                await apiClient.post(
                    "/syllabuses",
                    syllabus
                );
            } else {
                await apiClient.put(
                    `/syllabuses/${syllabus.id}`,
                    syllabus
                );
            }

            return true;
        } catch (error) {
            console.error("Save syllabus failed:", error);
            return false;
        }
    }

    async deleteSyllabus(id) {
        try {
            await apiClient.delete(`/syllabuses/${id}`);
            return true;
        } catch (error) {
            console.error("Delete syllabus failed:", error);
            return false;
        }
    }

    // 4. CLASSES (Lớp học - Staff)

    async getClasses() {
        try {
            const res = await apiClient.get("/classes");

            return res
                ? res.map(item => new ClassDto(item))
                : this.getMockClasses();
        } catch {
            return this.getMockClasses();
        }
    }

    async saveClass(cls, isNew) {
        try {
            if (isNew) {
                await apiClient.post("/classes", cls);
            } else {
                await apiClient.put(
                    `/classes/${cls.id}`,
                    cls
                );
            }

            return true;
        } catch (error) {
            console.error("Save class failed:", error);
            return false;
        }
    }

    async deleteClass(id) {
        try {
            await apiClient.delete(`/classes/${id}`);
            return true;
        } catch (error) {
            console.error("Delete class failed:", error);
            return false;
        }
    }

    // 5. PROJECTS (Duyệt dự án - Head Department)

    async getProjects() {
        try {
            const res = await apiClient.get("/projects");

            return res
                ? res.map(project => new ProjectDto(project))
                : this.getMockProjects();
        } catch {
            return this.getMockProjects();
        }
    }

    async updateProjectStatus(id, status, note = "") {
        try {
            await apiClient.patch(
                `/projects/${id}/status`,
                { status, note }
            );

            return true;
        } catch (error) {
            console.error("Update project status failed:", error);
            return false;
        }
    }

    // MOCK DATA

    getMockUsers() {
        return [
            new UserDto({
                id: "U001",
                fullName: "Nguyễn Văn An",
                email: "an.nv@ut.edu.vn",
                role: "Admin",
                isActive: true
            }),

            new UserDto({
                id: "U002",
                fullName: "Trần Thị Bình",
                email: "binh.tt@ut.edu.vn",
                role: "Head",
                isActive: true
            }),

            new UserDto({
                id: "U003",
                fullName: "Lê Hoàng Cường",
                email: "cuong.lh@ut.edu.vn",
                role: "Staff",
                isActive: true
            }),

            new UserDto({
                id: "U004",
                fullName: "Phạm Minh Dung",
                email: "dung.pm@ut.edu.vn",
                role: "Lecturer",
                isActive: false
            })
        ];
    }

    getMockSubjects() {
        return [
            new SubjectDto({
                id: "S01",
                code: "SE104",
                name: "Nhập môn Công nghệ phần mềm",
                credits: 4,
                department: "CNPM"
            }),

            new SubjectDto({
                id: "S02",
                code: "CS112",
                name: "Phân tích và thiết kế thuật toán",
                credits: 4,
                department: "KHMT"
            }),

            new SubjectDto({
                id: "S03",
                code: "IS201",
                name: "Phân tích thiết kế hệ thống thông tin",
                credits: 4,
                department: "HTTT"
            })
        ];
    }

    getMockSyllabuses() {
        return [
            new SyllabusDto({
                id: "SYL01",
                subjectCode: "SE104",
                subjectName: "Nhập môn Công nghệ phần mềm",
                version: "2026.1",
                description: "Quy trình phát triển phần mềm Agile/Scrum",
                status: "Đã duyệt"
            }),

            new SyllabusDto({
                id: "SYL02",
                subjectCode: "CS112",
                subjectName: "Phân tích và thiết kế thuật toán",
                version: "2026.1",
                description: "Quy hoạch động, tham lam, đồ thị",
                status: "Chờ duyệt"
            })
        ];
    }

    getMockClasses() {
        return [
            new ClassDto({
                id: "C01",
                classCode: "SE104.O21",
                subjectCode: "SE104",
                lecturerName: "ThS. Nguyễn Văn A",
                semester: "HK1-2026",
                studentCount: 45
            }),

            new ClassDto({
                id: "C02",
                classCode: "CS112.O22",
                subjectCode: "CS112",
                lecturerName: "TS. Lê Thị B",
                semester: "HK1-2026",
                studentCount: 50
            })
        ];
    }

    getMockProjects() {
        return [
            new ProjectDto({
                id: "PRJ01",
                name: "Hệ thống quản lý học tập CollabSphere",
                groupName: "Nhóm 01 - SE104.O21",
                lecturerName: "ThS. Nguyễn Văn A",
                status: "Chờ duyệt"
            }),

            new ProjectDto({
                id: "PRJ02",
                name: "Ứng dụng đặt lịch phòng khám",
                groupName: "Nhóm 04 - SE104.O21",
                lecturerName: "ThS. Nguyễn Văn A",
                status: "Đã duyệt"
            })
        ];
    }
}

const collabSphereApiService = new CollabSphereApiService();

export default collabSphereApiService;
