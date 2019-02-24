using System.ComponentModel.DataAnnotations;


namespace QuizData
{
    public class EntityBase
    {
        [Key]
        public virtual int ID { get; set; }
    }
}