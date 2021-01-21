using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _30seconds.Models
{
    public class Room
    {
        public Room()
        {
            Created = DateTime.Now;
            LastPing = DateTime.Now;
            AmountOfSeconds = 30;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public int IdWordlist { get; set; }

        [ForeignKey(nameof(IdWordlist))]
        public virtual Wordlist Wordlist { get; set; }

        public int AmountOfSeconds { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastPing { get; set; }
    }
}
