using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData.Models
{
    public class QuestionType
    {
        [Key]
        public int QuestionTypeID { get; set; }
        
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        public string QuestionTypeName { get; set; }
    }
}