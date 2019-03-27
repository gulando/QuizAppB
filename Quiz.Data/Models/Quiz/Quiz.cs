using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    
    public class Quiz : EntityBase
    {
        [Column("QuizID")]
        public override int ID { get; set; }
        
        public string QuizName { get; set; }
        
    }

    public class QuizSummary : Quiz
    {
        public int QuizThemeID { get; set; }
        
        public int QuestionTypeID { get; set; }
        
        public int AnswerTypeID { get; set; }
        
        public string QuizThemeName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        public string AnswerTypeName { get; set; }
    }
}