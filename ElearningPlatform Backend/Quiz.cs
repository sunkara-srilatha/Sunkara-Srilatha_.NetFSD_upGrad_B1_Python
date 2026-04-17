using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElearningPlatform.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        [ForeignKey("CourseId")]
        public int CourseId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        // Navigation properties
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Result> Results { get; set; }

        public Quiz()
        {
            Questions = new HashSet<Question>();
            Results = new HashSet<Result>();
        }
    }
}
