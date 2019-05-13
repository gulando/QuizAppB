using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class AnswerType : EntityBase
    {
        [Column("AnswerTypeID")]
        public override int ID { get; set; }
        
        [Required]
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        [Required]
        [ForeignKey("QuestionType")]
        public int QuestionTypeID { get; set; }
        
        [Required]
        public string AnswerTypeName { get; set; }
    }

    public class AnswerTypeSummary : AnswerType
    {
        public string QuizName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
    }

    public enum AnswerTypes
    {
        None = 0,
        OptionalResponse = 1,
        ShortAnswerFirstType =2,
        ShortAnswerSecondType = 3,
        ResponseByBeam = 4
    }
}