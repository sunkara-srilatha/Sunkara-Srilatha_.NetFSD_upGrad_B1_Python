using System.ComponentModel.DataAnnotations;

namespace ElearningPlatform.DTOs
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
    }

    public class CreateQuestionDto
    {
        [Required(ErrorMessage = "Quiz ID is required")]
        public int QuizId { get; set; }

        [Required(ErrorMessage = "Question text is required")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Option A is required")]
        [StringLength(255, ErrorMessage = "Option A cannot exceed 255 characters")]
        public string OptionA { get; set; }

        [Required(ErrorMessage = "Option B is required")]
        [StringLength(255, ErrorMessage = "Option B cannot exceed 255 characters")]
        public string OptionB { get; set; }

        [Required(ErrorMessage = "Option C is required")]
        [StringLength(255, ErrorMessage = "Option C cannot exceed 255 characters")]
        public string OptionC { get; set; }

        [Required(ErrorMessage = "Option D is required")]
        [StringLength(255, ErrorMessage = "Option D cannot exceed 255 characters")]
        public string OptionD { get; set; }

        [Required(ErrorMessage = "Correct answer is required")]
        [StringLength(1, ErrorMessage = "Correct answer must be a single character")]
        [RegularExpression("^[A-D]$", ErrorMessage = "Correct answer must be A, B, C, or D")]
        public string CorrectAnswer { get; set; }
    }

    public class UpdateQuestionDto
    {
        [Required(ErrorMessage = "Question text is required")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Option A is required")]
        [StringLength(255, ErrorMessage = "Option A cannot exceed 255 characters")]
        public string OptionA { get; set; }

        [Required(ErrorMessage = "Option B is required")]
        [StringLength(255, ErrorMessage = "Option B cannot exceed 255 characters")]
        public string OptionB { get; set; }

        [Required(ErrorMessage = "Option C is required")]
        [StringLength(255, ErrorMessage = "Option C cannot exceed 255 characters")]
        public string OptionC { get; set; }

        [Required(ErrorMessage = "Option D is required")]
        [StringLength(255, ErrorMessage = "Option D cannot exceed 255 characters")]
        public string OptionD { get; set; }

        [Required(ErrorMessage = "Correct answer is required")]
        [StringLength(1, ErrorMessage = "Correct answer must be a single character")]
        [RegularExpression("^[A-D]$", ErrorMessage = "Correct answer must be A, B, C, or D")]
        public string CorrectAnswer { get; set; }
    }

    public class QuizAttemptDto
    {
        public int QuizId { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
    }
}
