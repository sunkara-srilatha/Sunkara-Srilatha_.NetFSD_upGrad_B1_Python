using ElearningPlatform.Data;
using ElearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningPlatform.Repositories
{
    public class QuizRepository : Repository<Quiz>, IQuizRepository
    {
        public QuizRepository(ElearningDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByCourseAsync(int courseId)
        {
            return await _dbSet.AsNoTracking()
                .Where(q => q.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<Quiz> GetQuizWithQuestionsAsync(int quizId)
        {
            return await _dbSet.AsNoTracking()
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.QuizId == quizId);
        }
    }
}
