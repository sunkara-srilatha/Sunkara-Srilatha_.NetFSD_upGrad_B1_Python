using Microsoft.AspNetCore.Mvc;
using ElearningPlatform.DTOs;
using ElearningPlatform.Services;

namespace ElearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(IQuestionService questionService, ILogger<QuestionsController> logger)
        {
            _questionService = questionService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            try
            {
                var question = await _questionService.GetQuestionByIdAsync(id);
                return Ok(question);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Question not found: {QuestionId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving question: {QuestionId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("quiz/{quizId}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestionsByQuiz(int quizId)
        {
            try
            {
                var questions = await _questionService.GetQuestionsByQuizAsync(quizId);
                return Ok(questions);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Quiz not found: {QuizId}", quizId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving questions by quiz: {QuizId}", quizId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var question = await _questionService.CreateQuestionAsync(createQuestionDto);
                return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionId }, question);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Quiz not found: {QuizId}", createQuestionDto.QuizId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating question");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<QuestionDto>> UpdateQuestion(int id, UpdateQuestionDto updateQuestionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var question = await _questionService.UpdateQuestionAsync(id, updateQuestionDto);
                return Ok(question);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Question not found: {QuestionId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating question: {QuestionId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            try
            {
                var deleted = await _questionService.DeleteQuestionAsync(id);
                if (!deleted)
                {
                    return NotFound($"Question with ID {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting question: {QuestionId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
