using System.ComponentModel.DataAnnotations;

namespace ElearningPlatform.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LessonCount { get; set; }
        public int QuizCount { get; set; }
    }

    public class CreateCourseDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Creator ID is required")]
        public int CreatedBy { get; set; }
    }

    public class UpdateCourseDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }
    }

    public class CourseDetailDto : CourseDto
    {
        public List<LessonDto> Lessons { get; set; }
        public List<QuizDto> Quizzes { get; set; }
    }
}
