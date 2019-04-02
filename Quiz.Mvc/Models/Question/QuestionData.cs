namespace QuizMvc.Models
{
    public class QuestionData
    {
        public int ID { get; set; }
        
        public int QuizID { get; set; }
        
        public int QuizThemeID { get; set; }
        
        public int AnswerTypeID { get; set; }
        
        public int QuestionTypeID { get; set; }
        
        public int ImageID { get; set; }
        
        public string QuizName { get; set; }
        
        public string QuizThemeName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        public string AnswerTypeName { get; set; }
        
        public string CorrectAnswer { get; set; }
    }
}