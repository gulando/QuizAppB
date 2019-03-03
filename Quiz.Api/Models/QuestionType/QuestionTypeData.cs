namespace QuizApi.Models
{
    public class QuestionTypeData
    {
        public int ID { get; set; }
                
        public int QuizID { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        public string QuizName { get; set; }
    }
}