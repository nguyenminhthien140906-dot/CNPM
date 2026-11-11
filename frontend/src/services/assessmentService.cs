
using CollabSphere.Domain.Entities;
using CollabSphere.Domain.Enums;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.Infrastructure.Services
{
    public interface IAssessmentService
    {
        Task<List<Evaluation>> GetAllAsync();
        Task<Evaluation?> GetByIdAsync(string id);
        Task<Evaluation> CreateAsync(Evaluation evaluation);
        Task<bool> DeleteAsync(string id);
    }

    public class AssessmentService : IAssessmentService
    {
        private readonly ApplicationDbContext _context;

        public AssessmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Evaluation>> GetAllAsync()
        {
            return await _context.Evaluations
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Evaluation?> GetByIdAsync(string id)
        {
            return await _context.Evaluations
                .FirstOrDefaultAsync(x => x.EvalId == id);
        }

        public async Task<Evaluation> CreateAsync(
            Evaluation evaluation)
        {
            if (evaluation.Score < 0 || evaluation.Score > 10)
                throw new ArgumentException(
                    "Điểm đánh giá phải từ 0 đến 10.");

            if (evaluation.EvaluatorId == evaluation.TargetId)
                throw new ArgumentException(
                    "Không thể tự đánh giá bản thân.");

            if (string.IsNullOrWhiteSpace(evaluation.EvalId))
                evaluation.EvalId = Guid.NewGuid().ToString();

            evaluation.CreatedAt = DateTime.UtcNow;

            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();

            return evaluation;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var evaluation = await GetByIdAsync(id);

            if (evaluation == null)
                return false;

            _context.Evaluations.Remove(evaluation);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

