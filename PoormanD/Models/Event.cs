using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoormanD.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Context { get; set; }
        public string Identifier { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public TimeSpan Duration { get; set; }

        [Column(TypeName = "JSON")]
        public string Parameters { get; set; }

        public static string JsonExtract(string jsonString, string path)
        {
            throw new NotImplementedException();
        }
    }
}