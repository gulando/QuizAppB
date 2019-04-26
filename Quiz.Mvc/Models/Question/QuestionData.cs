using System.ComponentModel.DataAnnotations;

namespace QuizMvc.Models
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
        
        [Required]
        [Display(Name = "Answer Type")]
        public int AnswerTypeID { get; set; }
        
        [Required]
        [Display(Name = "Question Type")]
        public int QuestionTypeID { get; set; }

        [Required]
        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        [Required]
        [Display(Name = "Correct Answer")]
        public string CorrectAnswer { get; set; }

        public int ImageID { get; set; }
        
        public string QuizName { get; set; }
        
        public string QuizThemeName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        public string AnswerTypeName { get; set; }
      
    }
}