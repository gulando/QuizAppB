using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class QuestionData
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Quiz Name")]
        public int QuizID { get; set; }
        
        [Required]
        [Display(Name = "Quiz Theme Name")]
        public int QuizThemeID { get; set; }
        
        public string QuizName { get; set; }
        
        public string QuizThemeName { get; set; }
              
        [Required]
        [Display(Name = "Correct Answer")]
        public string CorrectAnswer { get; set; }
    }
}