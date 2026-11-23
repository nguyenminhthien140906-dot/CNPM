
using CollabSphere.Infrastructure.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.Infrastructure.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project project);
        Task<Project?> UpdateAsync(int id, Project data);
        Task<bool> DeleteAsync(int id);
    }

    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects
                .AsNoTracking()
                .Include(p => p.Owner)
                .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Owner)
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> CreateAsync(Project project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
                throw new ArgumentException(
                    "Tên dự án không được để trống.");

            var ownerExists = await _context.Users
                .AnyAsync(u => u.Id == project.OwnerId);

            if (!ownerExists)
                throw new Exception("Không tìm thấy người tạo dự án.");

            project.Id = 0;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project?> UpdateAsync(
            int id, Project data)
        {
            var project = await _context.Projects
                .FindAsync(id);

            if (project == null)
                return null;

            if (string.IsNullOrWhiteSpace(data.Name))
                throw new ArgumentException(
                    "Tên dự án không được để trống.");

            project.Name = data.Name;
            project.Description = data.Description;

            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects
                .FindAsync(id);

            if (project == null)
                return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

