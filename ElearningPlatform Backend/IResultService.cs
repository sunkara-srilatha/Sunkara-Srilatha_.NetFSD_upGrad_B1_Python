using ElearningPlatform.DTOs;

namespace ElearningPlatform.Services
{
    public interface IResultService
    {
        Task<QuizSubmissionResultDto> SubmitQuizAsync(int quizId, int userId, QuizAttemptDto quizAttempt);
        Task<ResultDto> GetResultByIdAsync(int resultId);
        Task<IEnumerable<ResultDto>> GetResultsByUserAsync(int userId);
        Task<IEnumerable<ResultDto>> GetResultsByQuizAsync(int quizId);
        Task<bool> DeleteResultAsync(int resultId);
    }
}
