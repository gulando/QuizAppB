using System.ComponentModel.DataAnnotations.Schema;

namespace QuizData
{
    public class RoleRight : BussinessEntityBase
    {
        [Column("RoleRightID")]
        public override int ID { get; set; }
        
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        
        [ForeignKey("Right")]
        public int RightID { get; set; }
        
    }
}