using System.ComponentModel.DataAnnotations;

namespace ElearningPlatform.DTOs
{
    public class QuizDto
    {
        public int QuizId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int QuestionCount { get; set; }
    }

    public class CreateQuizDto
    {
        [Required(ErrorMessage = "Course ID is required")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }
    }

    public class UpdateQuizDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }
    }

    public class QuizDetailDto : QuizDto
    {
        public List<QuestionDto> Questions { get; set; }
    }
}
