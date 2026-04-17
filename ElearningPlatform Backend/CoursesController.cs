using Microsoft.AspNetCore.Mvc;
using ElearningPlatform.DTOs;
using ElearningPlatform.Services;

namespace ElearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseService courseService, ILogger<CoursesController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving courses");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(id);
                return Ok(course);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Course not found: {CourseId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving course: {CourseId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<CourseDetailDto>> GetCourseWithDetails(int id)
        {
            try
            {
                var course = await _courseService.GetCourseWithDetailsAsync(id);
                return Ok(course);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Course not found: {CourseId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving course details: {CourseId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto createCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var course = await _courseService.CreateCourseAsync(createCourseDto);
                return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Creator not found: {CreatorId}", createCourseDto.CreatedBy);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating course");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var course = await _courseService.UpdateCourseAsync(id, updateCourseDto);
                return Ok(course);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Course not found: {CourseId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course: {CourseId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                var deleted = await _courseService.DeleteCourseAsync(id);
                if (!deleted)
                {
                    return NotFound($"Course with ID {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course: {CourseId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("creator/{creatorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByCreator(int creatorId)
        {
            try
            {
                var courses = await _courseService.GetCoursesByCreatorAsync(creatorId);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving courses by creator: {CreatorId}", creatorId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
