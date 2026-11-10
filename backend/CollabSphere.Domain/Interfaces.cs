using System.Collections.Generic;
using CollabSphere.Domain.Entities;
using DomainTask = CollabSphere.Domain.Entities.Task;

namespace CollabSphere.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetById(string userId);
        void Save(User user);
    }

    public interface ITaskRepository
    {
        IEnumerable<DomainTask> GetTasksByGroup(string groupId);
        void Save(DomainTask task);
    }

    public interface IWorkspaceRepository
    {
        Workspace GetByGroup(string groupId);
        void SaveSnapshot(Workspace workspace);
    }
}
