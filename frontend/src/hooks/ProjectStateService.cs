
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollabSphere.Client.Hooks;

public class ProjectStateService
{
    private readonly List<ProjectState> _projects = new();

    private int? _selectedProjectId;

    public IReadOnlyList<ProjectState> Projects => _projects;

    public ProjectState? SelectedProject =>
        _projects.FirstOrDefault(
            x => x.ProjectId == _selectedProjectId
        );

    public int? SelectedProjectId => _selectedProjectId;

    public event Action? OnProjectStateChanged;

    public void SetProjects(IEnumerable<ProjectState> projects)
    {
        if (projects == null)
            throw new ArgumentNullException(nameof(projects));

        _projects.Clear();
        _projects.AddRange(projects);

        if (!_projects.Any(x => x.ProjectId == _selectedProjectId))
        {
            _selectedProjectId = _projects.FirstOrDefault()?.ProjectId;
        }

        NotifyStateChanged();
    }

    public void AddProject(ProjectState project)
    {
        if (project == null)
            throw new ArgumentNullException(nameof(project));

        if (_projects.Any(x => x.ProjectId == project.ProjectId))
            return;

        _projects.Add(project);

        if (_selectedProjectId == null)
        {
            _selectedProjectId = project.ProjectId;
        }

        NotifyStateChanged();
    }

    public void SelectProject(int projectId)
    {
        var project = _projects.FirstOrDefault(
            x => x.ProjectId == projectId
        );

        if (project == null)
            return;

        _selectedProjectId = projectId;

        NotifyStateChanged();
    }

    public ProjectState? GetProject(int projectId)
    {
        return _projects.FirstOrDefault(
            x => x.ProjectId == projectId
        );
    }

    public void UpdateProject(ProjectState updatedProject)
    {
        if (updatedProject == null)
            throw new ArgumentNullException(nameof(updatedProject));

        var existingProject = GetProject(
            updatedProject.ProjectId
        );

        if (existingProject == null)
            return;

        existingProject.ProjectCode = updatedProject.ProjectCode;
        existingProject.ProjectName = updatedProject.ProjectName;
        existingProject.Description = updatedProject.Description;
        existingProject.Status = updatedProject.Status;
        existingProject.Progress = updatedProject.Progress;
        existingProject.StartDate = updatedProject.StartDate;
        existingProject.EndDate = updatedProject.EndDate;

        NotifyStateChanged();
    }

    public void UpdateProjectStatus(int projectId, string status)
    {
        var project = GetProject(projectId);

        if (project == null)
            return;

        project.Status = status;

        NotifyStateChanged();
    }

    public void UpdateProgress(int projectId, int progress)
    {
        var project = GetProject(projectId);

        if (project == null)
            return;

        project.Progress = Math.Clamp(progress, 0, 100);

        NotifyStateChanged();
    }

    public void RemoveProject(int projectId)
    {
        var project = GetProject(projectId);

        if (project == null)
            return;

        _projects.Remove(project);

        if (_selectedProjectId == projectId)
        {
            _selectedProjectId = _projects.FirstOrDefault()?.ProjectId;
        }

        NotifyStateChanged();
    }

    public void ClearProjects()
    {
        _projects.Clear();
        _selectedProjectId = null;

        NotifyStateChanged();
    }

    public void NotifyStateChanged()
    {
        OnProjectStateChanged?.Invoke();
    }
}

public class ProjectState
{
    public int ProjectId { get; set; }

    public string ProjectCode { get; set; } = "";

    public string ProjectName { get; set; } = "";

    public string Description { get; set; } = "";

    public string Status { get; set; } = "DRAFT";

    public int Progress { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? LecturerId { get; set; }

    public int? ClassId { get; set; }
}

