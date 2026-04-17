using AutoMapper;
using ElearningPlatform.DTOs;
using ElearningPlatform.Models;
using ElearningPlatform.Repositories;

namespace ElearningPlatform.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public QuizService(IQuizRepository quizRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<QuizDto> CreateQuizAsync(CreateQuizDto createQuizDto)
        {
            // Verify that the course exists
            if (!await _courseRepository.ExistsAsync(createQuizDto.CourseId))
            {
                throw new KeyNotFoundException($"Course with ID {createQuizDto.CourseId} not found");
            }

            var quiz = _mapper.Map<Quiz>(createQuizDto);
            var createdQuiz = await _quizRepository.AddAsync(quiz);
            
            return _mapper.Map<QuizDto>(createdQuiz);
        }

        public async Task<QuizDto> GetQuizByIdAsync(int quizId)
        {
            var quiz = await _quizRepository.GetByIdAsync(quizId);
            if (quiz == null)
            {
                throw new KeyNotFoundException($"Quiz with ID {quizId} not found");
            }

            return _mapper.Map<QuizDto>(quiz);
        }

        public async Task<QuizDetailDto> GetQuizWithQuestionsAsync(int quizId)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
            if (quiz == null)
            {
                throw new KeyNotFoundException($"Quiz with ID {quizId} not found");
            }

            return _mapper.Map<QuizDetailDto>(quiz);
        }

        public async Task<IEnumerable<QuizDto>> GetQuizzesByCourseAsync(int courseId)
        {
            // Verify that the course exists
            if (!await _courseRepository.ExistsAsync(courseId))
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found");
            }

            var quizzes = await _quizRepository.GetQuizzesByCourseAsync(courseId);
            return _mapper.Map<IEnumerable<QuizDto>>(quizzes);
        }

        public async Task<QuizDto> UpdateQuizAsync(int quizId, UpdateQuizDto updateQuizDto)
        {
            var existingQuiz = await _quizRepository.GetByIdAsync(quizId);
            if (existingQuiz == null)
            {
                throw new KeyNotFoundException($"Quiz with ID {quizId} not found");
            }

            _mapper.Map(updateQuizDto, existingQuiz);
            var updatedQuiz = await _quizRepository.UpdateAsync(existingQuiz);
            
            return _mapper.Map<QuizDto>(updatedQuiz);
        }

        public async Task<bool> DeleteQuizAsync(int quizId)
        {
            var quiz = await _quizRepository.DeleteAsync(quizId);
            return quiz != null;
        }
    }
}
