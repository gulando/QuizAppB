using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class QuestionType : BussinessEntityBase
    {
        [Column("QuestionTypeID")]
        public override int ID { get; set; }

        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        public string QuestionTypeName { get; set; }
    }

    public class QuestionTypeSummary : QuestionType
    {
        public string QuizName { get; set; }
        
    }
}