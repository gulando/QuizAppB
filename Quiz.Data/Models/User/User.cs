using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class User : EntityBase
    {
        [Column("UserID")]
        public override int ID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }
        
        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }
        
        /// <summary>
        //This is only for Quiz.Mvc to check that all functional related with UserManagement works fine, for real application
        // we will use Quiz.Api and for that we already have correct(JWT).
        /// </summary>
        public string Password { get; set; }
    }
}