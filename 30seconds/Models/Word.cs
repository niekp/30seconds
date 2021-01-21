using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _30seconds.Models
{
    public class Word
    {
        public Word() {
        }
        public Word(string Text, int IdWordlist) {
            this.Text = Text;
            this.IdWordlist = IdWordlist;
        }

        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int IdWordlist { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IdWordlist))]
        public virtual Wordlist Wordlist { get; set; }

        public string Text { get; set; }

    }
}
