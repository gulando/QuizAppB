using System.ComponentModel.DataAnnotations.Schema;
using QuizData.EntityBase;


namespace QuizData.Models
{
    
    public class Quiz : BussinessEntityBase
    {
        [Column("QuizID")]
        public override int ID { get; set; }
        
        public string QuizName { get; set; }
    }
}