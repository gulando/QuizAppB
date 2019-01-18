using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData.Models
{
    public class QuizTheme
    {
        public int QuizThemeID { get; set; }
        
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        public string QuizThemeName { get; set; }
    }
}