namespace QuizApi.Models
{
    public class AnswerTypeData
    {
        public int ID { get; set; }
        
        public int QuizID { get; set; }
        
        public string QuizName { get; set; }
        
        public int QuestionTypeID { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        public string AnswerTypeName { get; set; }
    }
}