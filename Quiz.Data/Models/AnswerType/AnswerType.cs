using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;


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

        [Column(TypeName = "xml")]
        public string AnswerTypeDescription { get; set; }

        [NotMapped]
        public XElement AnswerTypeDescriptionElement => XElement.Parse(AnswerTypeDescription);
    }

    public class AnswerTypeSummary : AnswerType
    {
        public string QuizName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
    }

    public enum RenderType
    {
        None = 0,
        CheckBox = 1,
        RadioGroup = 2
    }

    public class AnswerTypeConfiguration
    {
        public RenderType RenderType { get; set; }
        public int Count { get; set; }
        public int CorrectCount { get; set; }
    }
}