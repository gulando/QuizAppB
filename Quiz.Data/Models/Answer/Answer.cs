using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class Answer : EntityBase
    {
        [Column("AnswerID")]
        public override int ID { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        
        [ForeignKey("AnswerType")]
        public int AnswerTypeID { get; set; }
        
        public string AnswerText { get; set; }
    }

    public class AnswerSummary : Answer
    {
        public string QuestionName { get; set; }
        
        public string AnswerTypeName { get; set; }
    }
}