using Microsoft.AspNetCore.Mvc;
using ElearningPlatform.DTOs;
using ElearningPlatform.Services;

namespace ElearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly ILogger<QuizzesController> _logger;

        public QuizzesController(IQuizService quizService, ILogger<QuizzesController> logger)
        {
            _quizService = quizService;
            _logger = logger;
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizzesByCourse(int courseId)
        {
            try
            {
                var quizzes = await _quizService.GetQuizzesByCourseAsync(courseId);
                return Ok(quizzes);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Course not found: {CourseId}", courseId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quizzes by course: {CourseId}", courseId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("quiz/{quizId}")]
        public async Task<ActionResult<QuizDto>> GetQuiz(int quizId)
        {
            try
            {
                var quiz = await _quizService.GetQuizByIdAsync(quizId);
                return Ok(quiz);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Quiz not found: {QuizId}", quizId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quiz: {QuizId}", quizId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{quizId}/questions")]
        public async Task<ActionResult<QuizDetailDto>> GetQuizWithQuestions(int quizId)
        {
            try
            {
                var quiz = await _quizService.GetQuizWithQuestionsAsync(quizId);
                return Ok(quiz);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Quiz not found: {QuizId}", quizId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quiz with questions: {QuizId}", quizId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuizDto>> CreateQuiz(CreateQuizDto createQuizDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var quiz = await _quizService.CreateQuizAsync(createQuizDto);
                return CreatedAtAction(nameof(GetQuiz), new { quizId = quiz.QuizId }, quiz);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Course not found: {CourseId}", createQuizDto.CourseId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating quiz");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{quizId}")]
        public async Task<ActionResult<QuizDto>> UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var quiz = await _quizService.UpdateQuizAsync(quizId, updateQuizDto);
                return Ok(quiz);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Quiz not found: {QuizId}", quizId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating quiz: {QuizId}", quizId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{quizId}")]
        public async Task<ActionResult> DeleteQuiz(int quizId)
        {
            try
            {
                var deleted = await _quizService.DeleteQuizAsync(quizId);
                if (!deleted)
                {
                    return NotFound($"Quiz with ID {quizId} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting quiz: {QuizId}", quizId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
