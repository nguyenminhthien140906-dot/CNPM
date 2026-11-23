
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.Infrastructure.Services
{
    public interface ICheckpointService
    {
        Task<List<Checkpoint>> GetAllAsync();
        Task<Checkpoint?> GetByIdAsync(string id);
        Task<Checkpoint> CreateAsync(Checkpoint checkpoint);
        Task<bool> UpdateStatusAsync(string id, string status);
        Task<bool> DeleteAsync(string id);
    }

    public class CheckpointService : ICheckpointService
    {
        private readonly ApplicationDbContext _context;

        public CheckpointService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Checkpoint>> GetAllAsync()
        {
            return await _context.Checkpoints.ToListAsync();
        }

        public async Task<Checkpoint?> GetByIdAsync(string id)
        {
            return await _context.Checkpoints
                .FirstOrDefaultAsync(x => x.CheckpointId == id);
        }

        public async Task<Checkpoint> CreateAsync(
            Checkpoint checkpoint)
        {
            if (string.IsNullOrWhiteSpace(checkpoint.Title))
                throw new ArgumentException(
                    "Tên checkpoint không được để trống.");

            checkpoint.Status = "Pending";

            _context.Checkpoints.Add(checkpoint);
            await _context.SaveChangesAsync();

            return checkpoint;
        }

        public async Task<bool> UpdateStatusAsync(
            string id, string status)
        {
            var allowed = new[]
            {
                "Pending", "InProgress",
                "Submitted", "Approved", "Rejected"
            };

            if (!allowed.Contains(status))
                throw new ArgumentException(
                    "Trạng thái checkpoint không hợp lệ.");

            var checkpoint = await GetByIdAsync(id);

            if (checkpoint == null)
                return false;

            checkpoint.Status = status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var checkpoint = await GetByIdAsync(id);

            if (checkpoint == null)
                return false;

            _context.Checkpoints.Remove(checkpoint);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

