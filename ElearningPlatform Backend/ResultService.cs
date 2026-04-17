using AutoMapper;
using ElearningPlatform.DTOs;
using ElearningPlatform.Models;
using ElearningPlatform.Repositories;

namespace ElearningPlatform.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public ResultService(IResultRepository resultRepository, IQuizRepository quizRepository, 
            IUserRepository userRepository, IQuestionRepository questionRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _quizRepository = quizRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<QuizSubmissionResultDto> SubmitQuizAsync(int quizId, int userId, QuizAttemptDto quizAttempt)
        {
            // Verify that the quiz and user exist
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
            if (quiz == null)
            {
                throw new KeyNotFoundException($"Quiz with ID {quizId} not found");
            }

            if (!await _userRepository.ExistsAsync(userId))
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            // Validate that all questions belong to the quiz
            var questionIds = quiz.Questions.Select(q => q.QuestionId).ToList();
            var submittedQuestionIds = quizAttempt.Answers.Select(a => a.QuestionId).ToList();

            if (submittedQuestionIds.Any(id => !questionIds.Contains(id)))
            {
                throw new ArgumentException("One or more questions do not belong to this quiz");
            }

            // Calculate score
            var score = 0;
            var questionResults = new List<QuestionResultDto>();

            foreach (var question in quiz.Questions)
            {
                var userAnswer = quizAttempt.Answers.FirstOrDefault(a => a.QuestionId == question.QuestionId);
                var isCorrect = userAnswer != null && 
                               userAnswer.SelectedAnswer.ToUpper() == question.CorrectAnswer.ToUpper();

                if (isCorrect)
                {
                    score++;
                }

                questionResults.Add(new QuestionResultDto
                {
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText,
                    CorrectAnswer = question.CorrectAnswer,
                    SelectedAnswer = userAnswer?.SelectedAnswer ?? "",
                    IsCorrect = isCorrect
                });
            }

            // Create result
            var result = new Result
            {
                UserId = userId,
                QuizId = quizId,
                Score = score,
                AttemptDate = DateTime.Now
            };

            var createdResult = await _resultRepository.AddAsync(result);

            // Return submission result
            var totalQuestions = quiz.Questions.Count;
            var percentage = totalQuestions > 0 ? (decimal)score / totalQuestions * 100 : 0;
            var grade = GetGrade(percentage);

            return new QuizSubmissionResultDto
            {
                ResultId = createdResult.ResultId,
                Score = score,
                TotalQuestions = totalQuestions,
                Percentage = percentage,
                Grade = grade,
                AttemptDate = createdResult.AttemptDate,
                QuestionResults = questionResults
            };
        }

        public async Task<ResultDto> GetResultByIdAsync(int resultId)
        {
            var result = await _resultRepository.GetResultWithDetailsAsync(resultId);
            if (result == null)
            {
                throw new KeyNotFoundException($"Result with ID {resultId} not found");
            }

            return _mapper.Map<ResultDto>(result);
        }

        public async Task<IEnumerable<ResultDto>> GetResultsByUserAsync(int userId)
        {
            // Verify that the user exists
            if (!await _userRepository.ExistsAsync(userId))
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            var results = await _resultRepository.GetResultsByUserAsync(userId);
            return _mapper.Map<IEnumerable<ResultDto>>(results);
        }

        public async Task<IEnumerable<ResultDto>> GetResultsByQuizAsync(int quizId)
        {
            // Verify that the quiz exists
            if (!await _quizRepository.ExistsAsync(quizId))
            {
                throw new KeyNotFoundException($"Quiz with ID {quizId} not found");
            }

            var results = await _resultRepository.GetResultsByQuizAsync(quizId);
            return _mapper.Map<IEnumerable<ResultDto>>(results);
        }

        public async Task<bool> DeleteResultAsync(int resultId)
        {
            var result = await _resultRepository.DeleteAsync(resultId);
            return result != null;
        }

        private string GetGrade(decimal percentage)
        {
            return percentage switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
        }
    }
}
