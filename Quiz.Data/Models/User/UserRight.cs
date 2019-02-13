using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class UserRight : BussinessEntityBase
    {
        [Column("UserRightID")]
        public override int ID { get; set; }
        
        [ForeignKey("User")]
        public int UserID { get; set; }
        
        [ForeignKey("Right")]
        public int RightID { get; set; }
    }
}