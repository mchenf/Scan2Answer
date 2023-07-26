namespace Scan2Answer.Models
{
    public class QuestionSession
    {
        public Question? Question { get; set; }
        public string? SessionId { get; set; }
        public long StudentsOnline { get; set; } = 0;
    }
}
