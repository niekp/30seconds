using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _30seconds.Models
{
    public class Game
    {
        public Game()
        {
            Words = new List<Word>();
        }

        [Key]
        public int Id { get; set; }

        public int IdRoom { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IdRoom))]
        public virtual Room Room { get; set; }

        public virtual ICollection<Word> Words { get; set; }

        public DateTime Start { get; set; }

        public string User { get; set; }

    }
}
