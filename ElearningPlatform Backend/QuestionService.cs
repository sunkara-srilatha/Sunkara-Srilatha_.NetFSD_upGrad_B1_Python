using AutoMapper;
using ElearningPlatform.DTOs;
using ElearningPlatform.Models;
using ElearningPlatform.Repositories;

namespace ElearningPlatform.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IQuizRepository quizRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto createQuestionDto)
        {
            // Verify that the quiz exists
            if (!await _quizRepository.ExistsAsync(createQuestionDto.QuizId))
            {
                throw new KeyNotFoundException($"Quiz with ID {createQuestionDto.QuizId} not found");
            }

            var question = _mapper.Map<Question>(createQuestionDto);
            var createdQuestion = await _questionRepository.AddAsync(question);
            
            return _mapper.Map<QuestionDto>(createdQuestion);
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(int questionId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null)
            {
                throw new KeyNotFoundException($"Question with ID {questionId} not found");
            }

            return _mapper.Map<QuestionDto>(question);
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsByQuizAsync(int quizId)
        {
            // Verify that the quiz exists
            if (!await _quizRepository.ExistsAsync(quizId))
            {
                throw new KeyNotFoundException($"Quiz with ID {quizId} not found");
            }

            var questions = await _questionRepository.GetQuestionsByQuizAsync(quizId);
            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async Task<QuestionDto> UpdateQuestionAsync(int questionId, UpdateQuestionDto updateQuestionDto)
        {
            var existingQuestion = await _questionRepository.GetByIdAsync(questionId);
            if (existingQuestion == null)
            {
                throw new KeyNotFoundException($"Question with ID {questionId} not found");
            }

            _mapper.Map(updateQuestionDto, existingQuestion);
            var updatedQuestion = await _questionRepository.UpdateAsync(existingQuestion);
            
            return _mapper.Map<QuestionDto>(updatedQuestion);
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            var question = await _questionRepository.DeleteAsync(questionId);
            return question != null;
        }
    }
}
