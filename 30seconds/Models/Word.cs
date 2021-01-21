using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int IdWordlist { get; set; }

        [ForeignKey(nameof(IdWordlist))]
        public virtual Wordlist Wordlist { get; set; }

        public string Text { get; set; }

    }
}
