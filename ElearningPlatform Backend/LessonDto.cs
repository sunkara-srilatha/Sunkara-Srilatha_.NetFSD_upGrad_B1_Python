using System.ComponentModel.DataAnnotations;

namespace ElearningPlatform.DTOs
{
    public class LessonDto
    {
        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int OrderIndex { get; set; }
    }

    public class CreateLessonDto
    {
        [Required(ErrorMessage = "Course ID is required")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        public string Content { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Order index must be greater than 0")]
        public int OrderIndex { get; set; }
    }

    public class UpdateLessonDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        public string Content { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Order index must be greater than 0")]
        public int OrderIndex { get; set; }
    }
}
