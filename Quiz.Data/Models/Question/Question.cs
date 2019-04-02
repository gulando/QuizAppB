using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class Question : EntityBase
    {
        [Column("QuestionID")]
        public override int ID { get; set; }

        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        [ForeignKey("QuizTheme")]
        public int QuizThemeID { get; set; }
        
        [ForeignKey("AnswerType")]
        public int AnswerTypeID { get; set; }
        
        [ForeignKey("QuestionType")]
        public int QuestionTypeID { get; set; }
                        
        public string CorrectAnswer { get; set; }
    }

    public class QuestionSummary : Question
    {
        public int ImageID { get; set; } 
        
        public string QuizName { get; set; }
        
        public string QuizThemeName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        public string AnswerTypeName { get; set; }
    }
}