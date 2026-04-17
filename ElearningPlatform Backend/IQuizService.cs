using ElearningPlatform.DTOs;

namespace ElearningPlatform.Services
{
    public interface IQuizService
    {
        Task<QuizDto> CreateQuizAsync(CreateQuizDto createQuizDto);
        Task<QuizDto> GetQuizByIdAsync(int quizId);
        Task<QuizDetailDto> GetQuizWithQuestionsAsync(int quizId);
        Task<IEnumerable<QuizDto>> GetQuizzesByCourseAsync(int courseId);
        Task<QuizDto> UpdateQuizAsync(int quizId, UpdateQuizDto updateQuizDto);
        Task<bool> DeleteQuizAsync(int quizId);
    }
}
