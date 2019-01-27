using System.ComponentModel.DataAnnotations;


namespace QuizData
{
    public class BussinessEntityBase
    {
        [Key]
        public virtual int ID { get; set; }
    }
}