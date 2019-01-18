using System.ComponentModel.DataAnnotations;


namespace QuizData.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }
        
        [Required]
        public string QuizName { get; set; }
        
    }
}