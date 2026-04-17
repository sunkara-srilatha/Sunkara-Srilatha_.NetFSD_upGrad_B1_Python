using ElearningPlatform.Models;

namespace ElearningPlatform.Repositories
{
    public interface IResultRepository : IRepository<Result>
    {
        Task<IEnumerable<Result>> GetResultsByUserAsync(int userId);
        Task<IEnumerable<Result>> GetResultsByQuizAsync(int quizId);
        Task<Result> GetResultWithDetailsAsync(int resultId);
    }
}
