using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class Image : EntityBase
    {
        [Column("ImageID")]
        public override int ID { get; set; }
        
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public long Length { get; set; }

        public string ContentType { get; set; }

    }
}