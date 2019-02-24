using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class UserRight : EntityBase
    {
        [Column("UserRightID")]
        public override int ID { get; set; }
        
        [ForeignKey("User")]
        public int UserID { get; set; }
        
        [ForeignKey("Right")]
        public int RightID { get; set; }
    }

    public class UserRightSummary : UserRight
    {
        public string UserName { get; set; }
        
        public string RightName { get; set; }
    }
}