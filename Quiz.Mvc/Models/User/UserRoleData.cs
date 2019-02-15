using System.Collections.Generic;
using QuizData;


namespace QuizMvc.Models
{
    public class UserRoleData
    {
        public int ID { get; set; }
        
        public int UserID { get; set; }
        
        public string UserName { get; set; }
        
        public int RoleID { get; set; }
        
        public string RoleName { get; set; }
        
        public List<User> Users { get; set; }
        
        public List<Role> Roles { get; set; }
    }
}