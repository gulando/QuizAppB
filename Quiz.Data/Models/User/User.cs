

using System.ComponentModel.DataAnnotations.Schema;

namespace QuizData
{
    public class User : BussinessEntityBase
    {
        [Column("UserID")]
        public override int ID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }
        
        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }
    }
}