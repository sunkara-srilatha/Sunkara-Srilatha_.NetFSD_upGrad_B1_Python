using ElearningPlatform.DTOs;

namespace ElearningPlatform.Services
{
    public interface IQuestionService
    {
        Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto createQuestionDto);
        Task<QuestionDto> GetQuestionByIdAsync(int questionId);
        Task<IEnumerable<QuestionDto>> GetQuestionsByQuizAsync(int quizId);
        Task<QuestionDto> UpdateQuestionAsync(int questionId, UpdateQuestionDto updateQuestionDto);
        Task<bool> DeleteQuestionAsync(int questionId);
    }
}
