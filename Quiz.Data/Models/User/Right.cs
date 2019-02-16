using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class Right : BussinessEntityBase
    {
        [Column("RightID")]
        public override int ID { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}