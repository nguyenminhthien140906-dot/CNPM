
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.Infrastructure.Services
{
    public interface ITeamService
    {
        Task<List<Group>> GetAllAsync();
        Task<Group?> GetByIdAsync(string id);
        Task<Group> CreateAsync(
            string id, string name,
            string courseId, string leaderId);
        Task<bool> AddMemberAsync(string groupId, string studentId);
        Task<bool> RemoveMemberAsync(string groupId, string studentId);
    }

    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group?> GetByIdAsync(string id)
        {
            return await _context.Groups
                .FirstOrDefaultAsync(g => g.GroupId == id);
        }

        public async Task<Group> CreateAsync(
            string id, string name,
            string courseId, string leaderId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên nhóm không được để trống.");

            var group = new Group(
                id, name, courseId, leaderId);

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return group;
        }

        public async Task<bool> AddMemberAsync(
            string groupId, string studentId)
        {
            var group = await GetByIdAsync(groupId);

            if (group == null)
                return false;

            group.AddMember(studentId);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveMemberAsync(
            string groupId, string studentId)
        {
            var group = await GetByIdAsync(groupId);

            if (group == null)
                return false;

            group.RemoveMember(studentId);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

