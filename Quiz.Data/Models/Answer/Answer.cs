using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuizData.EntityBase;


namespace QuizData.Models
{
    public class Answer : BussinessEntityBase
    {
        [Column("AnswerID")]
        public override int ID { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        
        [ForeignKey("AnswerType")]
        public int AnswerTypeID { get; set; }
        
        public string AnswerText { get; set; }
    }
}