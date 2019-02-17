using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class UserRole : BussinessEntityBase
    {
        [Column("UserRoleID")]
        public override int ID { get; set; }
        
        [ForeignKey("User")]
        public int UserID { get; set; }
        
        [ForeignKey("Role")]
        public int RoleID { get; set; }
    }

    public class UserRoleSummary : UserRole
    {
        public string UserName { get; set; }
        
        public string RoleName { get; set; }
    }
}