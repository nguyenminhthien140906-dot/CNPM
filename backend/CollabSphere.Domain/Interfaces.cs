using System.Collections.Generic;
using CollabSphere.Domain.Entities;

namespace CollabSphere.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetById(string userId);
        void Save(User user);
    }

    public interface ITaskRepository
    {
        IEnumerable<Task> GetTasksByGroup(string groupId);
        void Save(Task task);
    }

    public interface IWorkspaceRepository
    {
        Workspace GetByGroup(string groupId);
        void SaveSnapshot(Workspace workspace);
    }
}
