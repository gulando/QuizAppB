using System.Collections.Generic;
using QuizData;


namespace QuizMvc.Models
{
    public class UserData
    {
        public int ID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
    }
    
    public class UserRoleData
    {
        public int ID { get; set; }
        
        public int UserID { get; set; }
        
        public string UserName { get; set; }
        
        public int RoleID { get; set; }
        
        public string RoleName { get; set; }
        
    }
    
    public class UserRightData
    {
        public int ID { get; set; }
        
        public int UserID { get; set; }
        
        public string UserName { get; set; }
        
        public int RightID { get; set; }
        
        public string RightName { get; set; }
        
    }

    public class RoleRightData
    {
        public int ID { get; set; }
        
        public int RoleID { get; set; }
        
        public string RoleName { get; set; }
        
        public int RightID { get; set; }
        
        public string RightName { get; set; }

    }
    
}