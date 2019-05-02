using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class ExamType : EntityBase
    {
        [Column("ExamTypeID")]
        public override int ID { get; set; }

        public string ExamTypeName { get; set; }
    }
}
