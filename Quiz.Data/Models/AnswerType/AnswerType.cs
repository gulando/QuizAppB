using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData.Models
{
    public class AnswerType
    {
        [Key]
        public int AnswerTypeID { get; set; }
        
        [ForeignKey("Quiz")]
        public int QuizTD { get; set; }
        
        public string AnswerTypeName { get; set; }
    }
}