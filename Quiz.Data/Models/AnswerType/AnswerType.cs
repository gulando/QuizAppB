using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuizData.EntityBase;


namespace QuizData.Models
{
    public class AnswerType : BussinessEntityBase
    {
        [Column("AnswerTypeID")]
        public override int ID { get; set; }
        
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        public string AnswerTypeName { get; set; }
    }
}