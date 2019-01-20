using System.ComponentModel.DataAnnotations.Schema;
using QuizData.EntityBase;


namespace QuizData.Models
{
    public class QuestionType : BussinessEntityBase
    {
        [Column("QuestionTypeID")]
        public override int ID { get; set; }

        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        public string QuestionTypeName { get; set; }
    }
}