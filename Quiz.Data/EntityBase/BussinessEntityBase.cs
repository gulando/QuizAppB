using System.ComponentModel.DataAnnotations;


namespace QuizData.EntityBase
{
    public class BussinessEntityBase
    {
        [Key]
        public virtual int ID { get; set; }
    }
}