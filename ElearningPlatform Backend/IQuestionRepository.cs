using ElearningPlatform.Models;

namespace ElearningPlatform.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<IEnumerable<Question>> GetQuestionsByQuizAsync(int quizId);
    }
}
