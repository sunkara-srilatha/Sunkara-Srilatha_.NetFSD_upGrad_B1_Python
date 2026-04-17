using Microsoft.AspNetCore.Mvc;
using ElearningPlatform.DTOs;
using ElearningPlatform.Services;

namespace ElearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultService _resultService;
        private readonly ILogger<ResultsController> _logger;

        public ResultsController(IResultService resultService, ILogger<ResultsController> logger)
        {
            _resultService = resultService;
            _logger = logger;
        }

        [HttpPost("quiz/{quizId}/submit")]
        public async Task<ActionResult<QuizSubmissionResultDto>> SubmitQuiz(int quizId, [FromBody] QuizAttemptDto quizAttempt, [FromQuery] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _resultService.SubmitQuizAsync(quizId, userId, quizAttempt);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Quiz or User not found: QuizId={QuizId}, UserId={UserId}", quizId, userId);
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid quiz submission: QuizId={QuizId}, UserId={UserId}", quizId, userId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting quiz: QuizId={QuizId}, UserId={UserId}", quizId, userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{resultId}")]
        public async Task<ActionResult<ResultDto>> GetResult(int resultId)
        {
            try
            {
                var result = await _resultService.GetResultByIdAsync(resultId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Result not found: {ResultId}", resultId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving result: {ResultId}", resultId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ResultDto>>> GetResultsByUser(int userId)
        {
            try
            {
                var results = await _resultService.GetResultsByUserAsync(userId);
                return Ok(results);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "User not found: {UserId}", userId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving results by user: {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("quiz/{quizId}/results")]
        public async Task<ActionResult<IEnumerable<ResultDto>>> GetResultsByQuiz(int quizId)
        {
            try
            {
                var results = await _resultService.GetResultsByQuizAsync(quizId);
                return Ok(results);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Quiz not found: {QuizId}", quizId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving results by quiz: {QuizId}", quizId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{resultId}")]
        public async Task<ActionResult> DeleteResult(int resultId)
        {
            try
            {
                var deleted = await _resultService.DeleteResultAsync(resultId);
                if (!deleted)
                {
                    return NotFound($"Result with ID {resultId} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting result: {ResultId}", resultId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
