namespace ElearningPlatform.DTOs
{
    public class ResultDto
    {
        public int ResultId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public decimal Percentage { get; set; }
        public DateTime AttemptDate { get; set; }
    }

    public class CreateResultDto
    {
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int Score { get; set; }
    }

    public class QuizSubmissionResultDto
    {
        public int ResultId { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public decimal Percentage { get; set; }
        public string Grade { get; set; }
        public DateTime AttemptDate { get; set; }
        public List<QuestionResultDto> QuestionResults { get; set; }
    }

    public class QuestionResultDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }
        public string SelectedAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
