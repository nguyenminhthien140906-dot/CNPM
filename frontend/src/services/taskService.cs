
using CollabSphere.Infrastructure.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.Infrastructure.Services
{
    public interface ITaskService
    {
        Task<List<WorkTask>> GetByProjectAsync(int projectId);
        Task<WorkTask> CreateAsync(WorkTask task);
        Task<WorkTask?> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAsync(int id);
    }

    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkTask>> GetByProjectAsync(
            int projectId)
        {
            return await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<WorkTask> CreateAsync(WorkTask task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException(
                    "Tên công việc không được để trống.");

            var projectExists = await _context.Projects
                .AnyAsync(p => p.Id == task.ProjectId);

            if (!projectExists)
                throw new Exception("Dự án không tồn tại.");

            task.Id = 0;
            task.CreatedAt = DateTime.UtcNow;
            task.Status = "Pending";

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<WorkTask?> UpdateStatusAsync(
            int id, string status)
        {
            var allowedStatuses = new[]
            {
                "Pending",
                "InProgress",
                "Review",
                "Completed"
            };

            if (!allowedStatuses.Contains(status))
                throw new ArgumentException(
                    "Trạng thái công việc không hợp lệ.");

            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return null;

            task.Status = status;

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

