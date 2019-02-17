using System.Collections.Generic;
using QuizData;


namespace QuizMvc.Models
{
    public class QuizThemeData
    {
        public int ID { get; set; }
                
        public int QuizID { get; set; }
        
        public string QuizThemeName { get; set; }
        
        public string QuizName { get; set; }
        
    }
}