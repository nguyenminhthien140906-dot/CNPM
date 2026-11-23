
import {
    useState,
    useMemo,
    useCallback
} from "react";

export const createProjectState = (data = {}) => ({
    projectId: data.projectId ?? 0,
    projectCode: data.projectCode || "",
    projectName: data.projectName || "",
    description: data.description || "",
    status: data.status || "DRAFT",
    progress: data.progress ?? 0,
    startDate: data.startDate || null,
    endDate: data.endDate || null,
    lecturerId: data.lecturerId || null,
    classId: data.classId ?? null
});

export const useProjectState = () => {
    const [projects, setProjects] = useState([]);
    const [selectedProjectId, setSelectedProjectId] =
        useState(null);

    const selectedProject = useMemo(() => {
        return projects.find(
            (project) =>
                project.projectId === selectedProjectId
        ) || null;
    }, [projects, selectedProjectId]);

    const setProjectsList = useCallback((data) => {
        if (!Array.isArray(data)) {
            throw new Error("Projects must be an array.");
        }

        const newProjects = data.map(createProjectState);

        setProjects(newProjects);

        setSelectedProjectId((currentId) => {
            const exists = newProjects.some(
                (project) => project.projectId === currentId
            );

            return exists
                ? currentId
                : newProjects[0]?.projectId ?? null;
        });
    }, []);

    const addProject = useCallback((project) => {
        if (!project) {
            throw new Error("Project cannot be empty.");
        }

        const newProject = createProjectState(project);

        setProjects((prev) => {
            if (
                prev.some(
                    (item) =>
                        item.projectId === newProject.projectId
                )
            ) {
                return prev;
            }

            return [...prev, newProject];
        });

        setSelectedProjectId((currentId) =>
            currentId === null
                ? newProject.projectId
                : currentId
        );
    }, []);

    const selectProject = useCallback((projectId) => {
        setProjects((prev) => {
            const exists = prev.some(
                (project) => project.projectId === projectId
            );

            if (exists) {
                setSelectedProjectId(projectId);
            }

            return prev;
        });
    }, []);

    const getProject = useCallback((projectId) => {
        return projects.find(
            (project) => project.projectId === projectId
        ) || null;
    }, [projects]);

    const updateProject = useCallback((updatedProject) => {
        if (!updatedProject) {
            throw new Error("Project cannot be empty.");
        }

        setProjects((prev) =>
            prev.map((project) =>
                project.projectId === updatedProject.projectId
                    ? {
                        ...project,
                        ...updatedProject
                    }
                    : project
            )
        );
    }, []);

    const updateProjectStatus = useCallback((
        projectId,
        status
    ) => {
        setProjects((prev) =>
            prev.map((project) =>
                project.projectId === projectId
                    ? { ...project, status }
                    : project
            )
        );
    }, []);

    const updateProgress = useCallback((
        projectId,
        progress
    ) => {
        const validProgress = Math.min(
            100,
            Math.max(0, Number(progress) || 0)
        );

        setProjects((prev) =>
            prev.map((project) =>
                project.projectId === projectId
                    ? {
                        ...project,
                        progress: validProgress
                    }
                    : project
            )
        );
    }, []);

    const removeProject = useCallback((projectId) => {
        setProjects((prev) => {
            const updated = prev.filter(
                (project) => project.projectId !== projectId
            );

            if (selectedProjectId === projectId) {
                setSelectedProjectId(
                    updated[0]?.projectId ?? null
                );
            }

            return updated;
        });
    }, [selectedProjectId]);

    const clearProjects = useCallback(() => {
        setProjects([]);
        setSelectedProjectId(null);
    }, []);

    return {
        projects,
        selectedProject,
        selectedProjectId,

        setProjects: setProjectsList,
        addProject,
        selectProject,
        getProject,
        updateProject,
        updateProjectStatus,
        updateProgress,
        removeProject,
        clearProjects
    };
};

export default useProjectState;
