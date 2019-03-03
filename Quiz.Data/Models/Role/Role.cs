using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class Role : EntityBase
    {
        [Column("RoleID")]
        public override int ID { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}