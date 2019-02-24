using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class AnswerType : EntityBase
    {
        [Column("AnswerTypeID")]
        public override int ID { get; set; }
        
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        [ForeignKey("QuestionType")]
        public int QuestionTypeID { get; set; }
        
        public string AnswerTypeName { get; set; }
    }

    public class AnswerTypeSummary : AnswerType
    {
        public string QuizName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
    }
}