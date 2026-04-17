using Microsoft.AspNetCore.Mvc;
using ElearningPlatform.DTOs;
using ElearningPlatform.Services;

namespace ElearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly ILogger<LessonsController> _logger;

        public LessonsController(ILessonService lessonService, ILogger<LessonsController> logger)
        {
            _lessonService = lessonService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLesson(int id)
        {
            try
            {
                var lesson = await _lessonService.GetLessonByIdAsync(id);
                return Ok(lesson);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Lesson not found: {LessonId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving lesson: {LessonId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsByCourse(int courseId)
        {
            try
            {
                var lessons = await _lessonService.GetLessonsByCourseAsync(courseId);
                return Ok(lessons);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Course not found: {CourseId}", courseId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving lessons by course: {CourseId}", courseId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LessonDto>> CreateLesson(CreateLessonDto createLessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var lesson = await _lessonService.CreateLessonAsync(createLessonDto);
                return CreatedAtAction(nameof(GetLesson), new { id = lesson.LessonId }, lesson);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Course not found: {CourseId}", createLessonDto.CourseId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating lesson");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LessonDto>> UpdateLesson(int id, UpdateLessonDto updateLessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var lesson = await _lessonService.UpdateLessonAsync(id, updateLessonDto);
                return Ok(lesson);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Lesson not found: {LessonId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating lesson: {LessonId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLesson(int id)
        {
            try
            {
                var deleted = await _lessonService.DeleteLessonAsync(id);
                if (!deleted)
                {
                    return NotFound($"Lesson with ID {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting lesson: {LessonId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
