using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData.Models
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        [ForeignKey("QuizTheme")]
        public int QuizThemeID { get; set; }
        
        [ForeignKey("AnswerType")]
        public int AnswerTypeID { get; set; }
        
        [ForeignKey("QuestionType")]
        public int QuestionTypeID { get; set; }
                
        public byte[] QuestionImage { get; set; }
        
        public string CorrectAnswer { get; set; }
    }
}