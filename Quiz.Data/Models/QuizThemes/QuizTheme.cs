using System.ComponentModel.DataAnnotations.Schema;
using QuizData.EntityBase;


namespace QuizData.Models
{
    public class QuizTheme : BussinessEntityBase
    {
        [Column("QuizThemeID")]
        public override int ID { get; set; }

        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        public string QuizThemeName { get; set; }
    }

    public class QuizThemeSummary : QuizTheme
    {
        public string QuizName { get; set; }
    }
}