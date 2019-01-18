using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData.Models
{
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
        
        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        
        [ForeignKey("AnswerType")]
        public int AnswerTypeID { get; set; }
        
        public string AnswerText { get; set; }
    }
}