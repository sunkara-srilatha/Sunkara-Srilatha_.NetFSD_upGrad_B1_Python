using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElearningPlatform.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [ForeignKey("QuizId")]
        public int QuizId { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        [StringLength(255)]
        public string OptionA { get; set; }

        [Required]
        [StringLength(255)]
        public string OptionB { get; set; }

        [Required]
        [StringLength(255)]
        public string OptionC { get; set; }

        [Required]
        [StringLength(255)]
        public string OptionD { get; set; }

        [Required]
        [StringLength(1)]
        public string CorrectAnswer { get; set; }

        // Navigation properties
        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }
    }
}
