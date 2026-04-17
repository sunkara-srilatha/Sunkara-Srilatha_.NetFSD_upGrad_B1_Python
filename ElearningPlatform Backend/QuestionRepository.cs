using ElearningPlatform.Data;
using ElearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningPlatform.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(ElearningDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Question>> GetQuestionsByQuizAsync(int quizId)
        {
            return await _dbSet.AsNoTracking()
                .Where(q => q.QuizId == quizId)
                .ToListAsync();
        }
    }
}
