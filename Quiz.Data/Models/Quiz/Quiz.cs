using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    
    public class Quiz : BussinessEntityBase
    {
        [Column("QuizID")]
        public override int ID { get; set; }
        
        public string QuizName { get; set; }
        
        public IEnumerable<QuizTheme> QuizThemes { get; set; }
    }
}