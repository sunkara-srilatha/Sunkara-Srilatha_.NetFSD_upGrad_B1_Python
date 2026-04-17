using ElearningPlatform.Data;
using ElearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningPlatform.Repositories
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        public ResultRepository(ElearningDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Result>> GetResultsByUserAsync(int userId)
        {
            return await _dbSet.AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.Quiz)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.AttemptDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetResultsByQuizAsync(int quizId)
        {
            return await _dbSet.AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.Quiz)
                .Where(r => r.QuizId == quizId)
                .OrderByDescending(r => r.AttemptDate)
                .ToListAsync();
        }

        public async Task<Result> GetResultWithDetailsAsync(int resultId)
        {
            return await _dbSet.AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.Quiz)
                    .ThenInclude(q => q.Questions)
                .FirstOrDefaultAsync(r => r.ResultId == resultId);
        }
    }
}
