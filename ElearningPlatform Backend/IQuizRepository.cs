using ElearningPlatform.Models;

namespace ElearningPlatform.Repositories
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        Task<IEnumerable<Quiz>> GetQuizzesByCourseAsync(int courseId);
        Task<Quiz> GetQuizWithQuestionsAsync(int quizId);
    }
}
